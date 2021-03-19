using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.ElementProperties
{
    public abstract class AFunctionProperty<TVarProp, TNamespace, TInitialisation, TDataType> :
        IFunctionProperty<TVarProp, TNamespace, TInitialisation, TDataType>
        where TVarProp : class, IVariableProperty<TDataType, TNamespace, TInitialisation>
        where TNamespace : class, INamespace
        where TInitialisation : class, IInitialisation
        where TDataType : class, IDataType<TNamespace>
    {
        protected TVarProp[] _arguments = new TVarProp[0];

        public AFunctionProperty()
        {
            Name = "<null>";
        }

        public AFunctionProperty(
            string name,
            TDataType? membership = null,
            TNamespace? namespace_ = null,
            TVarProp? return_ = null,
            params TVarProp[] args)
        {
            Name = name;
            Membership = membership;
            Namespace = namespace_;
            Return = return_;
            _arguments = args;
        }

        public abstract void Render(CodeWriter writer);

        public string Name { get; protected set; }
        public IEnumerable<TVarProp> Arguments { get => _arguments; }
        public TNamespace? Namespace { get; protected set; }
        public TVarProp? Return { get; protected set; }
        public TDataType? Membership { get; protected set; }
    }
}
