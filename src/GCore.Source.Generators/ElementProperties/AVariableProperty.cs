using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.ElementProperties
{
    public class AVariableProperty<TNamespace, TInitialisation> :
        IVariableProperty<TNamespace, TInitialisation>
        where TNamespace : INamespace
        where TInitialisation : IInitialisation
    {
        public AVariableProperty() { }

        public AVariableProperty(
            string name,
            TNamespace namespace_ = default(TNamespace),
            TInitialisation initialisation = default(TInitialisation))
        {
            Name = name;
            Namespace = namespace_;
            Initialisation = initialisation;
        }

        public string Name { get; protected set; }
        public TNamespace Namespace { get; protected set; }
        public TInitialisation Initialisation { get; protected set; }
    }
}