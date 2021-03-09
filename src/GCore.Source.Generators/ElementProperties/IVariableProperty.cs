using System;
using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.ElementProperties
{
    public interface IVariableProperty<TNamespace, TInitialisation> : 
        ISourceElementProperty
        where TNamespace : INamespace
        where TInitialisation : IInitialisation
    {
        String Name { get; }
        TNamespace Namespace { get; }
        TInitialisation Initialisation { get; }
    }
}