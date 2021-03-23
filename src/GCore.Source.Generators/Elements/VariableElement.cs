using GCore.Source.Generators.Elements.Components;

namespace GCore.Source.Generators.Elements
{
    public abstract class VariableElement : SourceElement
    {
        public bool IsArgument { get; }
        public IDataType? DataType { get; }
        public IInitialisation? Initialisation { get; protected set; }
        public VariableElement(SourceElement? parent, string name, IDataType? dataType = null, IInitialisation? init = null, bool isArgument = false) 
            : base(parent, name)
        {
            DataType = dataType;
            Initialisation = init;
            IsArgument = isArgument;
        }
    }
}