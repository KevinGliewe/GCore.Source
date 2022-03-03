using GCore.Source.Attributes;
using GCore.Source.Generators.Attributes;
using GCore.Source.Helper;

namespace GCore.Source.Generators.Elements
{
    [TaggedElement("RawAligned")]
    public class RawAlignedRaggedElement : RawTaggedElement
    {
        [Config("AlignTag")]  public char AlignTag { get; set; } = '§';

        public RawAlignedRaggedElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        public override void Render(CodeWriter writer)
        {
            SetLines(StringHelper.Align(Lines, AlignTag));

            base.Render(writer);
        }
    }
}