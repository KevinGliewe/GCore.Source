using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.ElementProperties
{
    public abstract class AFunctionProperty<TVarProp, TNamespace, TInitialisation, TDataType> :
        IFunctionProperty<TVarProp, TNamespace, TInitialisation, TDataType>
        where TVarProp : IVariableProperty<TNamespace, TInitialisation>
        where TNamespace : INamespace
        where TInitialisation : IInitialisation
        where TDataType : IDataType<TNamespace>
    {
        protected TVarProp[] _arguments = new TVarProp[0];

        public AFunctionProperty() { }

        public AFunctionProperty(
            string name,
            TDataType membership = default(TDataType),
            TNamespace namespace_ = default(TNamespace),
            TVarProp return_ = default(TVarProp),
            params TVarProp[] args)
        {
            Name = name;
            Membership = membership;
            Namespace = namespace_;
            Return = return_;
            _arguments = args;
        }

        public abstract void Render(IndentedTextWriter writer);

        public string Name { get; protected set; }
        public IEnumerable<TVarProp> Arguments { get; }
        public TNamespace Namespace { get; protected set; }
        public TVarProp Return { get; protected set; }
        public TDataType Membership { get; protected set; }
    }
}
