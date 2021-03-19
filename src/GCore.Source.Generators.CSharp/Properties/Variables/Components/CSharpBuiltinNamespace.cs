using System;
using System.Collections.Generic;
using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.CSharp.Properties.Variables.Components
{
    public class CSharpBuiltinNamespace : CSharpNamespace
    {

        public CSharpBuiltinNamespace(Type type)
        {
            Type = type;
        }

        public override void Render(CodeWriter writer)
        {
            writer.Write(Type.Namespace);
        }

        public Type Type { get; private set; }

        public override string Separator { get => "."; }
        public override IEnumerable<string> Namespaces
        {
            get => Type.Namespace.Split(Separator);
        }
    }
}