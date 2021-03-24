using System;
using GCore.Source.Attributes;
using GCore.Source.Generators.CSharp.Properties;
using GCore.Source.Generators.Elements;
using GCore.Source.Generators.Elements.Components;

namespace GCore.Source.Generators.CSharp.Elements
{
    public class CSharpVariable : VariableElement, ICSharpModifiable
    {
        [Config("Modifier")]
        public CSharpModifier Modifier { get; set; }

        public CSharpVariable(SourceElement? parent, string name, IDataType? dataType = null, IInitialisation? init = null, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false) 
            : base(parent, name, dataType, init, isArgument)
        {
            Modifier = modifier;
        }

        public CSharpVariable(SourceElement? parent, string name, Byte value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.Byte, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, SByte value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.SByte, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, Int32 value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.Int32, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, UInt32 value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.UInt32, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, Int16 value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.Int16, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, UInt16 value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.UInt16, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, Int64 value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.Int64, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, UInt64 value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.UInt64, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, IntPtr value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.IntPtr, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, UIntPtr value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.UIntPtr, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, Single value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.Single, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, Double value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.Double, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, Char value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.Char, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, Boolean value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.Boolean, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, Object value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.Object, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, String value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.String, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, Decimal value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.Decimal, new Initialisation(value), modifier, isArgument) { }

        public CSharpVariable(SourceElement? parent, string name, DateTime value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            : this(parent, name, CSharpType.DateTime, new Initialisation(value), modifier, isArgument) { }

        public override void Render(CodeWriter writer) {
            Modifier.Render(writer);

            if (DataType is null)
            {
                if (Parent is FunctionElement)
                    writer.Write("var");
                else
                    writer.Write("dynamic");
            }
            else
                DataType.Render(writer);


            writer.Write(' ');
            writer.Write(Name);

            if (Initialisation != null) {
                writer.Write(" = ");
                Initialisation?.Render(writer);
            }

            if (!IsArgument)
                writer.WriteLine(';');
        }

        public override string ToString()
            => Name;
    }
}