using GCore.Source.Generators.CSharp.Properties.Variables.Components;
using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.CSharp.Properties
{
    public abstract class CSharpType : ADataType<CSharpNamespace>, ICSharpProperty
    {
        public CSharpType(string name, CSharpNamespace? ns)
            : base(name, ns) { }

        public CSharpModifier Modifier { get; set; }
    }
}