using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GCore.Source.Extensions;
using GCore.Source.Generators.Attributes;
using GCore.Source.Generators.Elements;

namespace GCore.Source.Generators.Extensions
{
    public static class StringToSourceElements
    {
        public readonly static string STARTTAG_PATTERN = @"^.*<\[(?<tag>[^\/\s]+)\s*(?<parameters>.*)\]>\s*$";
        public readonly static string ENDTAG_PATTERN = @"^.*<\[\/(?<tag>\S+)\]>\s*$";

        public readonly static Regex StartTagRegex = new Regex(STARTTAG_PATTERN);
        public readonly static Regex EndTagRegex = new Regex(ENDTAG_PATTERN);

        public static SourceElement ParseToSourceElement(this string @this)
        {
            return @this.ParseToSourceElement(new SourceElement(null, "root"));
        }

        public static SourceElement ParseToSourceElement(this string @this, SourceElement root)
        {
            var lines = @this.SplitNewLine();

            var elemStack = new Stack<SourceElement>();

            elemStack.Push(root);

            var accumLines = new List<string>();

            for (int iLine = 0; iLine < lines.Length; iLine++)
            {
                var line = lines[iLine];

                var startMatch = StartTagRegex.Match(line);

                if (startMatch.Success)
                {
                    // Add all accumulated lines 
                    if (accumLines.Count > 0)
                    {
                        var accumLinesElem = new RawElement(elemStack.Peek(), "L" + (iLine-1));
                        accumLinesElem.SetLines(accumLines);
                        accumLines.Clear();
                        elemStack.Peek().Add(accumLinesElem);
                    }

                    var tag = startMatch.Groups["tag"].Value;
                    var parameters = startMatch.Groups["parameters"].Value;

                    var tagType = TaggedElementAttribute.GetFromTag(tag);

                    if (tagType is null)
                        throw new Exception($"Element tag '{tag}' not found! Line {iLine + 1}" );

                    var elem = Activator.CreateInstance(
                        tagType, args: 
                        new object[] { elemStack.Peek(), "L" + iLine }) 
                            as ITaggedElement;

                    var srcElem = elem as SourceElement;

                    if(elem is null)
                        throw new Exception($"Could not create instance of type '{tagType.FullName}'! Line {iLine + 1}");

                    if(srcElem is null)
                        throw new Exception($"Could not cast type '{tagType.FullName}' to SourceElement! Line {iLine + 1}");

                    elem.SetStartLine(line);
                    elem.Configure(parameters.ExtractParameters());

                    elemStack.Peek().Add(srcElem);

                    elemStack.Push(srcElem);

                    continue;
                }

                var endMatch = EndTagRegex.Match(line);

                if (endMatch.Success)
                {
                    // Add all accumulated lines 
                    if (accumLines.Count > 0) {
                        var accumLinesElem = new RawElement(elemStack.Peek(), "L" + (iLine - 1));
                        accumLinesElem.SetLines(accumLines);
                        accumLines.Clear();
                        elemStack.Peek().Add(accumLinesElem);
                    }

                    var currElem = elemStack.Pop();

                    (currElem as ITaggedElement)?.SetStopLine(line);

                    var tag = endMatch.Groups["tag"].Value;

                    var currElemTag = TaggedElementAttribute.GetTag(currElem.GetType());

                    if(tag != currElemTag)
                        throw new Exception($"Closing tag '{tag}' mismatch with open element {currElemTag}! Line {iLine + 1}");

                    continue;
                }

                accumLines.Add(line);
            }

            if (elemStack.Count > 1)
            {
                var remain = elemStack.Peek();
                throw new Exception($"Element '{remain.GetPath()}' of type '{remain.GetType().FullName}' was not closed!");
            }

            // Add all accumulated lines 
            if (accumLines.Count > 0) {
                var accumLinesElem = new RawElement(elemStack.Peek(), "L" + (lines.Length - 1));
                accumLinesElem.SetLines(accumLines);
                accumLines.Clear();
                elemStack.Peek().Add(accumLinesElem);
            }

            return root;
        }
    }
}