using System.Collections.Generic;

namespace GCore.Source.Generators.ElementProperties.PropertyComponents
{
    public interface INamespace : IRenderable
    {
        string Separator { get; }

        IEnumerable<string> Namespaces { get; }
    }
}