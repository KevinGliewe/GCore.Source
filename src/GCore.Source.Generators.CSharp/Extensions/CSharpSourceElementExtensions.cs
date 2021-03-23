using System;
using GCore.Source.Generators.CSharp.Elements;
using GCore.Source.Generators.CSharp.Properties;

using GCore.Source.Generators.Extensions;

namespace GCore.Source.Generators.CSharp.Extensions
{
    public static class CSharpSourceElementExtensions
    {
        public static SourceElement AddVar(this SourceElement @this, string name, Byte value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, SByte value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, Int32 value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, UInt32 value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, Int16 value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, UInt16 value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, Int64 value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, UInt64 value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, IntPtr value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, UIntPtr value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, Single value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, Double value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, Char value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, Boolean value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, Object value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, String value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, Decimal value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));

        public static SourceElement AddVar(this SourceElement @this, string name, DateTime value, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, value, modifier, isArgument));



        public static SourceElement AddVarByte(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
    => @this.Add(new CSharpVariable(@this, name, CSharpType.Byte, null, modifier, isArgument));

        public static SourceElement AddVarSByte(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.SByte, null, modifier, isArgument));

        public static SourceElement AddVarInt32(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.Int32, null, modifier, isArgument));

        public static SourceElement AddVarUInt32(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.UInt32, null, modifier, isArgument));

        public static SourceElement AddVarInt16(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.Int16, null, modifier, isArgument));

        public static SourceElement AddVarUInt16(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.UInt16, null, modifier, isArgument));

        public static SourceElement AddVarInt64(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.Int64, null, modifier, isArgument));

        public static SourceElement AddVarUInt64(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.UInt64, null, modifier, isArgument));

        public static SourceElement AddVarIntPtr(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.IntPtr, null, modifier, isArgument));

        public static SourceElement AddVarUIntPtr(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.UIntPtr, null, modifier, isArgument));

        public static SourceElement AddVarSingle(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.Single, null, modifier, isArgument));

        public static SourceElement AddVarDouble(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.Double, null, modifier, isArgument));

        public static SourceElement AddVarChar(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.Char, null, modifier, isArgument));

        public static SourceElement AddVarBoolean(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.Boolean, null, modifier, isArgument));

        public static SourceElement AddVarObject(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.Object, null, modifier, isArgument));

        public static SourceElement AddVarString(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.String, null, modifier, isArgument));

        public static SourceElement AddVarDecimal(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.Decimal, null, modifier, isArgument));

        public static SourceElement AddVarDateTime(this SourceElement @this, string name, CSharpModifier modifier = CSharpModifier.None, bool isArgument = false)
            => @this.Add(new CSharpVariable(@this, name, CSharpType.DateTime, null, modifier, isArgument));

    }
}