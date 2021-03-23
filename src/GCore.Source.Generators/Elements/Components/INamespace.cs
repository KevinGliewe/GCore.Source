using System.Collections.Generic;

namespace GCore.Source.Generators.Elements.Components
{
    public interface INamespace : IRenderable
    {
        string Separator { get; }

        IEnumerable<string> Namespaces { get; }
    }

    public interface INamespaceAble
    {
        INamespace? Namespace { get; }
    }
}