using System;

namespace GCore.Source.Generators.ElementProperties.PropertyComponents
{
    public interface IInitialisation : IRenderable
    {
        Object InitValue { get; set; }
    }
}