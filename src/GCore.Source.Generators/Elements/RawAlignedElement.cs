using System.Collections.Generic;
using GCore.Source.Helper;

namespace GCore.Source.Generators.Elements
{
    public class RawAlignedElement : RawElement
    {
        public char AlignTag { get; set; } = '§';

        public RawAlignedElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        public RawAlignedElement(SourceElement? parent, string name, string lines) : base(parent, name, lines)
        {
        }

        public RawAlignedElement(SourceElement? parent, string name, IEnumerable<string> lines) : base(parent, name, lines)
        {
        }

        public RawAlignedElement(SourceElement? parent, string name, params string[] lines) : base(parent, name, lines)
        {
        }

        public override void Render(CodeWriter writer)
        {
            SetLines(StringHelper.Align(Lines, AlignTag));

            base.Render(writer);
        }
    }
}