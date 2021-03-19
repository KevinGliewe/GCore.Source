using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.CSharp.Properties
{
    public abstract class CSharpInitialisation : IInitialisation
    {
        public abstract void Render(CodeWriter writer);

        public abstract object? InitValue { get; set; }
    }
}