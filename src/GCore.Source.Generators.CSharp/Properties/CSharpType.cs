using System;
using System.Dynamic;
using GCore.Source.Generators.Elements.Components;

namespace GCore.Source.Generators.CSharp.Properties
{
    public class CSharpType : DataType
    {
        public static readonly CSharpType Byte = new CSharpType(typeof(Byte));
        public static readonly CSharpType SByte = new CSharpType(typeof(SByte));
        public static readonly CSharpType Int32 = new CSharpType(typeof(Int32));
        public static readonly CSharpType UInt32 = new CSharpType(typeof(UInt32));
        public static readonly CSharpType Int16 = new CSharpType(typeof(Int16));
        public static readonly CSharpType UInt16 = new CSharpType(typeof(UInt16));
        public static readonly CSharpType Int64 = new CSharpType(typeof(Int64));
        public static readonly CSharpType UInt64 = new CSharpType(typeof(UInt64));
        public static readonly CSharpType IntPtr = new CSharpType(typeof(IntPtr));
        public static readonly CSharpType UIntPtr = new CSharpType(typeof(UIntPtr));
        public static readonly CSharpType Single = new CSharpType(typeof(Single));
        public static readonly CSharpType Double = new CSharpType(typeof(Double));
        public static readonly CSharpType Char = new CSharpType(typeof(Char));
        public static readonly CSharpType Boolean = new CSharpType(typeof(Boolean));
        public static readonly CSharpType Object = new CSharpType(typeof(Object));
        public static readonly CSharpType String = new CSharpType(typeof(String));
        public static readonly CSharpType Decimal = new CSharpType(typeof(Decimal));
        public static readonly CSharpType DateTime = new CSharpType(typeof(DateTime));
        public static readonly CSharpType Void = new CSharpType(typeof(void));
        public static readonly CSharpType Dynamic = new CSharpType(typeof(DynamicObject));


        public CSharpType(Type type) : base(type.Name, new CSharpNamespace(type))
        {
        }
    }
}