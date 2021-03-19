using System;
using System.Dynamic;
using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.CSharp.Properties.Variables.Components
{
    public class CSharpBuiltinType : CSharpType
    {
        public static readonly CSharpBuiltinType Byte = new CSharpBuiltinType(typeof(Byte));
        public static readonly CSharpBuiltinType SByte = new CSharpBuiltinType(typeof(SByte));
        public static readonly CSharpBuiltinType Int32 = new CSharpBuiltinType(typeof(Int32));
        public static readonly CSharpBuiltinType UInt32 = new CSharpBuiltinType(typeof(UInt32));
        public static readonly CSharpBuiltinType Int16 = new CSharpBuiltinType(typeof(Int16));
        public static readonly CSharpBuiltinType UInt16 = new CSharpBuiltinType(typeof(UInt16));
        public static readonly CSharpBuiltinType Int64 = new CSharpBuiltinType(typeof(Int64));
        public static readonly CSharpBuiltinType UInt64 = new CSharpBuiltinType(typeof(UInt64));
        public static readonly CSharpBuiltinType Single = new CSharpBuiltinType(typeof(Single));
        public static readonly CSharpBuiltinType Double = new CSharpBuiltinType(typeof(Double));
        public static readonly CSharpBuiltinType Char = new CSharpBuiltinType(typeof(Char));
        public static readonly CSharpBuiltinType Boolean = new CSharpBuiltinType(typeof(Boolean));
        public static readonly CSharpBuiltinType Object = new CSharpBuiltinType(typeof(Object));
        public static readonly CSharpBuiltinType String = new CSharpBuiltinType(typeof(String));
        public static readonly CSharpBuiltinType Decimal = new CSharpBuiltinType(typeof(Decimal));
        public static readonly CSharpBuiltinType DateTime = new CSharpBuiltinType(typeof(DateTime));
        public static readonly CSharpBuiltinType Void = new CSharpBuiltinType(typeof(void));
        public static readonly CSharpBuiltinType Dynamic = new CSharpBuiltinType(typeof(DynamicObject));


        public CSharpBuiltinType(Type type) 
            : base(type.Name, new CSharpBuiltinNamespace(type))
        {
            Type = type;
        }

        public Type Type { get; private set; }

        public override void Render(CodeWriter writer)
        {
            writer.Write(Type.FullName);
        }
    }
}