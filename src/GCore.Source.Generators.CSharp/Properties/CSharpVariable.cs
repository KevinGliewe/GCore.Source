using GCore.Source.Generators.CSharp.Properties.Variables;
using GCore.Source.Generators.CSharp.Properties.Variables.Components;
using GCore.Source.Generators.ElementProperties;

namespace GCore.Source.Generators.CSharp.Properties
{
    public abstract class CSharpVariable : AVariableProperty<CSharpType, CSharpNamespace, CSharpInitialisation>, ICSharpProperty
    {
        public CSharpVariable(string name,
            CSharpType? dataType = null,
            CSharpInitialisation? initialisation = null) 
            : base(name, dataType, initialisation) {}

        public CSharpModifier Modifier { get; set; }
        public abstract override void Render(CodeWriter writer);
    }
}