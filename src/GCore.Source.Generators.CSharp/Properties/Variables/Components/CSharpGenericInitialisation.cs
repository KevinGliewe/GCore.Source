using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.CSharp.Properties.Variables.Components
{
    public class CSharpGenericInitialisation : CSharpInitialisation
    {

        public CSharpGenericInitialisation(object init)
        {
            InitValue = init;
        }

        public override void Render(CodeWriter writer)
        {
            writer.Write(InitValue);
        }

        public override object? InitValue { get; set; }
    }
}