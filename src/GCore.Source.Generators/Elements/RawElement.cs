using System.Collections.Generic;
using System.Linq;
using GCore.Source.CodeContexts;
using GCore.Source.Extensions;

namespace GCore.Source.Generators.Elements
{
    public class RawElement : SourceElement, IRawElement
    {
        private string[]? lines = null;

        public string[] Lines {
            get {
                if (lines is null) {
                    var ret = new List<string>();
                    foreach (var child in ElementChildren) {
                        if (child is IRawElement rawChild) {
                            foreach (var line in rawChild.Lines) {
                                ret.Add(line);
                            }
                        }
                    }

                    return ret.ToArray();
                }

                return lines;
            }
        }

        public RawElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        public RawElement(SourceElement? parent, string name, string lines) : base(parent, name) {
            SetLines(lines);
        }

        public RawElement(SourceElement? parent, string name, IEnumerable<string> lines) : base(parent, name)
        {
            SetLines(lines);
        }

        public RawElement(SourceElement? parent, string name, params string[] lines) : base(parent, name)
        {
            SetLines(lines);
        }

        public virtual void SetLines(IEnumerable<string> lines)
        {
            this.lines = lines.ToArray();
        }

        public virtual void SetLines(string lines)
        {
            this.lines = lines.SplitNewLine();
        }

        public override void Render(CodeWriter writer)
        {
            using (new AbsoluteIndentContext(writer, AbsoluteIndent))
            {

                var l = Lines;

                for (int i = 0; i < l.Length; i++)
                {
                    writer.Write(l[i]);
                    if (i < l.Length - 1)
                        writer.WriteLine();
                }

            }
        }
    }
}