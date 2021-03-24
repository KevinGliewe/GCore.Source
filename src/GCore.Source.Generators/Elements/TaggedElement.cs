using System.Collections.Generic;
using System.Linq;
using GCore.Source.Generators.Attributes;

namespace GCore.Source.Generators.Elements
{

    [TaggedElement("T")]
    public class TaggedElement : SourceElement, ITaggedElement
    {
        protected string? _startLine;
        protected string? _stopLine;

        public TaggedElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        public override void Render(CodeWriter writer)
        {
            if (!(_startLine is null))
                writer.WriteLine(_startLine);

            var lengthBevore = writer.Length;

            base.Render(writer);

            var lengthAfter = writer.Length;

            if (lengthBevore < lengthAfter)
                writer.WriteLine();

            if (!(_stopLine is null))
                writer.Write(_stopLine);
        }

        public void SetStartLine(string start)
        {
            _startLine = start;
        }

        public void SetStopLine(string stop)
        {
            _stopLine = stop;
        }
    }
}