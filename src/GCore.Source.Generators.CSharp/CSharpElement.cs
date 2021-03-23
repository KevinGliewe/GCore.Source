namespace GCore.Source.Generators.CSharp
{
    public abstract class CSharpElement : SourceElement
    {
        public CSharpModifier Modifier { get; set; } = CSharpModifier.None;

        protected CSharpElement(SourceElement? parent, string name) : base(parent, name)
        {
        }
    }
}