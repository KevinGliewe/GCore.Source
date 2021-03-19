using System;
using GCore.Source.Generators.CSharp.Properties.Variables.Components;
using GCore.Source.Generators.ElementProperties;

namespace GCore.Source.Generators.CSharp.Properties.Variables
{
    public class CSharpGenericVariable : CSharpVariable
    {
        public CSharpGenericVariable(string name,
            CSharpGenericDataType? dataType = null,
            CSharpGenericInitialisation? initialisation = null) 
            : base(name, dataType, initialisation)
        {

        }

        public override void Render(CodeWriter writer)
        {
            Modifier.Render(writer);

            if (DataType is null)
                writer.Write("var");
            else
                DataType.Render(writer);


            writer.Write(' ');
            writer.Write(Name);

            if (Initialisation != null)
            {
                writer.Write(" = ");
                Initialisation?.Render(writer);
            }
        }

        public override string ToString()
            => Name;

        public CSharpModifier Modifier { get; set; } = CSharpModifier.None;
    }
}