using System.CodeDom.Compiler;

namespace GCore.Source.Generators.ElementProperties.PropertyComponents
{
    public abstract class ADataType<TNamespace> : 
        IDataType<TNamespace>
        where TNamespace : INamespace
    {

        public ADataType() { }

        public ADataType(string name, TNamespace ns = default(TNamespace))
        {
            Name = name;
            Namespace = ns;
        }

        public abstract void Render(IndentedTextWriter writer);


        public TNamespace Namespace { get; protected set; }

        public string Name { get; protected set; }
    }
}