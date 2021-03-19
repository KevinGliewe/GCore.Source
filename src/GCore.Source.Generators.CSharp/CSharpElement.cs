namespace GCore.Source.Generators.CSharp
{
    public abstract class CSharpElement : ASourceElement<CSharpTree, CSharpElement, ICSharpProperty>
    {
        public CSharpModifier Modifier { get; set; } = CSharpModifier.None;
    }
}