using System.CodeDom.Compiler;

namespace GCore.Source.Generators.ElementProperties.PropertyComponents
{
    public abstract class ADataType<TNamespace> : 
        IDataType<TNamespace>
        where TNamespace : class, INamespace
    {

        public ADataType()
        {
            Name = "<null>";
        }

        public ADataType(string name, TNamespace? ns = null)
        {
            Name = name;
            Namespace = ns;
        }

        public abstract void Render(CodeWriter writer);


        public TNamespace? Namespace { get; protected set; }

        public string Name { get; protected set; }
    }
}