using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Text;
using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.CSharp.Properties.Variables
{
    public class CSharpBuiltinInitialisation : CSharpInitialisation
    {
        private Object _value;

        public Dictionary<Type, Func<Object, string>> Formatters = new Dictionary<Type, Func<object, string>>();

        public CSharpBuiltinInitialisation(Object val)
        {
            _value = val;
        }

        public override void Render(CodeWriter writer)
        {

            if (_value is null) {
                writer.Write("null");
            } else if (_value is Byte vByte) {
                writer.Write(vByte.ToString(CultureInfo.InvariantCulture));
            } else if (_value is SByte vSByte) {
                writer.Write(vSByte.ToString(CultureInfo.InvariantCulture));
            } else if (_value is Int32 vInt32) {
                writer.Write(vInt32.ToString(CultureInfo.InvariantCulture));
            } else if (_value is UInt32 vUInt32) {
                writer.Write(vUInt32.ToString(CultureInfo.InvariantCulture));
            } else if (_value is Int16 vInt16) {
                writer.Write(vInt16.ToString(CultureInfo.InvariantCulture));
            } else if (_value is UInt16 vUInt16) {
                writer.Write(vUInt16.ToString(CultureInfo.InvariantCulture));
            } else if (_value is Int64 vInt64) {
                writer.Write(vInt64.ToString(CultureInfo.InvariantCulture));
            } else if (_value is UInt64 vUInt64) {
                writer.Write(vUInt64.ToString(CultureInfo.InvariantCulture));
            } else if (_value is Single vSingle) {
                writer.Write(vSingle.ToString(CultureInfo.InvariantCulture) + "f");
            } else if (_value is Double vDouble) {
                writer.Write(vDouble.ToString());
            } else if (_value is Char vChar) {
                writer.Write($"'{vChar}'");
            } else if (_value is Boolean vBoolean) {
                writer.Write(vBoolean ? "true" : "false");
            } else if (_value is String vString) {
                writer.Write(CodeWriter.ToLiteral(vString.ToString()));
            } else if (_value is Decimal vDecimal) {
                writer.Write(vDecimal.ToString(CultureInfo.InvariantCulture) + "m");
            } else if (_value is DateTime vDateTime) {
                writer.Write($"new DateTime({vDateTime.Ticks})");
            } else if(Formatters.ContainsKey(_value.GetType())) {
                writer.Write(Formatters[_value.GetType()]?.Invoke(_value) ?? _value.ToString());
            } else {
                writer.Write(_value.ToString());
            }
        }

        public override object? InitValue { get; set; }
    }
}
