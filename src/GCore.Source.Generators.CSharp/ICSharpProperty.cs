namespace GCore.Source.Generators.CSharp
{
    public interface ICSharpProperty : ISourceElementProperty
    {
        CSharpModifier Modifier { get; set; }

        void Render(CodeWriter writer);
    }
}