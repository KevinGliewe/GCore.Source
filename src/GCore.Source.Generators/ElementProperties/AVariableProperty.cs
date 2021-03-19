using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.ElementProperties
{
    public abstract class AVariableProperty<TDataType, TNamespace, TInitialisation> :
        IVariableProperty<TDataType, TNamespace, TInitialisation>
        where TDataType : class, IDataType<TNamespace>
        where TNamespace : class, INamespace
        where TInitialisation : class, IInitialisation
    {
        public AVariableProperty()
        {
            Name = "<null>";
        }

        public AVariableProperty(
            string name,
            TDataType? dataType = null,
            TInitialisation? initialisation = null)
        {
            Name = name;
            DataType = dataType;
            Initialisation = initialisation;
        }

        public string Name { get; protected set; }
        public TDataType? DataType { get; }
        public TInitialisation? Initialisation { get; protected set; }
        public abstract void Render(CodeWriter writer);
    }
}