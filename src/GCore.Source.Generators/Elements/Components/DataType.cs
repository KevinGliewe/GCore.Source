using System.CodeDom.Compiler;
using System.Collections.Generic;
using GCore.Source.CSharp;

namespace GCore.Source.Generators.Elements.Components
{
    public class DataType : IDataType
    {
        public INamespace? Namespace { get; protected set; }

        public string Name { get; protected set; }

        public DataType(string name, INamespace? ns)
        {
            Name = name;
            Namespace = ns;
        }

        public virtual void Render(CodeWriter writer)
        {
            if (!(Namespace is null))
            {
                Namespace.Render(writer);
                writer.Write(Namespace.Separator);
            }

            writer.Write(Name);
        }
    }
}