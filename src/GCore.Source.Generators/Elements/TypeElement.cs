using GCore.Source.Generators.Elements.Components;

namespace GCore.Source.Generators.Elements
{
    public abstract class TypeElement : SourceElement, INamespaceAble
    {

        public virtual IDataType? ResultingType => new DataType(this.Name, Namespace);
        public TypeElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        public abstract override void Render(CodeWriter writer);

        public abstract INamespace? Namespace { get; }
    }
}