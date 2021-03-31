using System;
using System.Collections.Generic;
using GCore.Source.Generators.Elements.Components;

namespace GCore.Source.Generators.CSharp.Properties
{
    public class CSharpNamespace : INamespace
    {
        private string[] _ns;

        public CSharpNamespace(params string[] ns) {
            _ns = ns;
        }

        public CSharpNamespace(Type type)
        {
            _ns = type.Namespace.Split(new string[] { Separator }, StringSplitOptions.None);
        }
        public void Render(CodeWriter writer) {
            writer.Write(String.Join(Separator, Namespaces));
        }

        public IEnumerable<string> Namespaces {
            get => _ns;
        }

        public string Separator => ".";
    }
}