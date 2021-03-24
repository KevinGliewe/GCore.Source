using System.Collections.Generic;
using System.Linq;
using GCore.Source.Attributes;
using GCore.Source.Extensions;
using GCore.Source.Generators.Attributes;

namespace GCore.Source.Generators.Elements
{
    [TaggedElement("Raw")]
    public class RawTaggedElement: TaggedElement, ITaggedElement, IRawElement
    {
        [Config("RenderTags")] public bool RenderTags { get; set; } = true;

        private string[]? lines = null;

        public string[] Lines {
            get {
                if (lines is null)
                {
                    var ret = new List<string>();
                    foreach (var child in ElementChildren)
                    {
                        if (child is IRawElement rawChild)
                        {
                            foreach (var line in rawChild.Lines)
                            {
                                ret.Add(line);
                            }
                        }
                    }

                    return ret.ToArray();
                }

                return lines;
            }
        }

        public RawTaggedElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        public void SetLines(IEnumerable<string> lines)
        {
            this.lines = lines.ToArray();
        }

        public void SetLines(string lines)
        {
            SetLines(lines.SplitNewLine());
        }

        public override void Render(CodeWriter writer) {
            if (!(_startLine is null) && RenderTags)
                writer.WriteLine(_startLine);

            writer.CurrentIndent += Indent;

            var l = Lines;

            for (int i = 0; i < l.Length; i++) {
                writer.Write(l[i]);
                if (i < l.Length - 1)
                    writer.WriteLine();
            }

            if (l.Length > 0)
                writer.WriteLine();

            writer.CurrentIndent -= Indent;

            if (!(_stopLine is null) && RenderTags)
                writer.Write(_stopLine);
        }
    }
}