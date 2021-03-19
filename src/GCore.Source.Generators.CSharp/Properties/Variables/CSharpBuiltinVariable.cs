using System;
using System.Collections.Generic;
using System.Text;
using GCore.Source.Generators.CSharp.Properties.Variables.Components;
using GCore.Source.Generators.ElementProperties;

namespace GCore.Source.Generators.CSharp.Properties.Variables
{
    public class CSharpBuiltinVariable : CSharpVariable
    {
        public CSharpBuiltinVariable(String name, Byte val) : base(name, CSharpBuiltinType.Byte, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, SByte val) : base(name, CSharpBuiltinType.SByte, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, Int32 val) : base(name, CSharpBuiltinType.Int32, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, UInt32 val) : base(name, CSharpBuiltinType.UInt32, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, Int16 val) : base(name, CSharpBuiltinType.Int16, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, UInt16 val) : base(name, CSharpBuiltinType.UInt16, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, Int64 val) : base(name, CSharpBuiltinType.Int64, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, UInt64 val) : base(name, CSharpBuiltinType.UInt64, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, Single val) : base(name, CSharpBuiltinType.Single, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, Double val) : base(name, CSharpBuiltinType.Double, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, Char val) : base(name, CSharpBuiltinType.Char, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, Boolean val) : base(name, CSharpBuiltinType.Boolean, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, String val) : base(name, CSharpBuiltinType.String, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, Decimal val) : base(name, CSharpBuiltinType.Decimal, new CSharpBuiltinInitialisation(val)) { }
        public CSharpBuiltinVariable(String name, DateTime val) : base(name, CSharpBuiltinType.DateTime, new CSharpBuiltinInitialisation(val)) { }


        public override void Render(CodeWriter writer)
        {
            Modifier.Render(writer);

            if (DataType is null)
                writer.Write("var");
            else
                DataType.Render(writer);


            writer.Write(' ');
            writer.Write(Name);

            if (Initialisation != null) {
                writer.Write(" = ");
                Initialisation?.Render(writer);
            }
        }

        public CSharpModifier Modifier { get; set; } = CSharpModifier.None;
    }
}
