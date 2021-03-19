using System;
using System.Collections.Generic;
using System.Text;
using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.ElementProperties
{
    public interface IFunctionProperty<TVarProp, TNamespace, TInitialisation, TDataType> :
        IRenderable
        where TVarProp : class, IVariableProperty<TDataType, TNamespace, TInitialisation>
        where TNamespace : class, INamespace
        where TInitialisation : class, IInitialisation
        where TDataType : class, IDataType<TNamespace>
    {
        string Name { get; }

        IEnumerable<TVarProp>? Arguments { get; }
        TNamespace? Namespace { get; }
        TVarProp? Return { get; }
        TDataType? Membership { get; }
    }
}
