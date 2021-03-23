using GCore.Source.Generators.Elements.Components;

namespace GCore.Source.Generators.Elements
{
    public abstract class FunctionElement : SourceElement
    {
        public abstract DataType? ReturnType { get; }
        public FunctionElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        public abstract override void Render(CodeWriter writer);
    }
}