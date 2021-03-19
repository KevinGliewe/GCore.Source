using System;
using System.Collections.Generic;
using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.CSharp.Properties.Variables.Components
{
    public class CSharpGenericNamespace : CSharpNamespace
    {
        private string[] _ns;

        public CSharpGenericNamespace(params string[] ns)
        {
            _ns = ns;
        }

        public override void Render(CodeWriter writer)
        {
            writer.Write(String.Join(Separator, Namespaces));
        }

        public override string Separator
        {
            get => ".";
        }
        public override IEnumerable<string> Namespaces
        {
            get => _ns;
        }
    }
}