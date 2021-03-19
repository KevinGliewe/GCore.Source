using System;
using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.ElementProperties
{
    public interface IVariableProperty<TDataType, TNamespace, TInitialisation> : 
        ISourceElementProperty, IRenderable
        where TDataType : class, IDataType<TNamespace>
        where TNamespace : class, INamespace
        where TInitialisation : class, IInitialisation
    {
        String Name { get; }
        TDataType? DataType { get; }
        TInitialisation? Initialisation { get; }
    }
}