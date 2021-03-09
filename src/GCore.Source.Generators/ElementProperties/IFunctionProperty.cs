using System;
using System.Collections.Generic;
using System.Text;
using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.ElementProperties
{
    public interface IFunctionProperty<TVarProp, TNamespace, TInitialisation, TDataType> :
        IRenderable
        where TVarProp : IVariableProperty<TNamespace, TInitialisation>
        where TNamespace : INamespace
        where TInitialisation : IInitialisation
        where TDataType : IDataType<TNamespace>
    {
        string Name { get; }

        IEnumerable<TVarProp> Arguments { get; }
        TNamespace Namespace { get; }
        TVarProp Return { get; }
        TDataType Membership { get; }
    }
}
