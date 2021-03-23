using System.Collections.Generic;
using System.Linq;
using GCore.Source.Extensions;

namespace GCore.Source.Generators.Elements
{
    public class RawElement : SourceElement
    {
        public string[] Lines { get; private set; } = new string[0];

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
            Lines = lines.ToArray();
        }

        public virtual void SetLines(string lines)
        {
            Lines = lines.SplitNewLine();
        }

        public override void Render(CodeWriter writer)
        {
            for (int i = 0; i < Lines.Length; i++) {
                writer.Write(Lines[i]);
                if (i < Lines.Length - 1)
                    writer.WriteLine();
            }
        }
    }
}