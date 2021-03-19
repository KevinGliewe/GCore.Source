using GCore.Source.Generators.ElementProperties.PropertyComponents;

namespace GCore.Source.Generators.CSharp.Properties.Variables.Components
{
    public class CSharpGenericDataType : CSharpType
    {
        public CSharpGenericDataType(string name, CSharpGenericNamespace? ns = null) : base(name, ns)
        {

        }

        public override void Render(CodeWriter writer)
        {
            if (Namespace != null)
            {
                Namespace.Render(writer);
                writer.Write(Namespace.Separator);
            }

            writer.Write(Name);
        }
    }
}