using System.Collections.Generic;
using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.CSharp.Properties
{
    public abstract class CSharpNamespace : INamespace
    {
        public abstract void Render(CodeWriter writer);

        public abstract string Separator { get; }
        public abstract IEnumerable<string> Namespaces { get; }
    }
}