using GCore.Source.Attributes;
using GCore.Source.CodeContexts;
using GCore.Source.Generators.Elements;
using GCore.Source.Generators.Elements.Components;

namespace GCore.Source.Generators.CSharp.Elements
{
    public class CSharpClass : TypeElement, ICSharpModifiable
    {
        [Config("Modifier")]
        public CSharpModifier Modifier { get; set; }

        [Config("IsStruct")]
        public bool IsStruct { get; set; } = false;

        public CSharpClass(SourceElement? parent, string name, INamespace? ns) : base(parent, name)
        {
            Indent = 4;
            Namespace = ns;
        }

        public override void Render(CodeWriter writer) {


            if (Namespace != null) {
                writer.Write("namespace ");
                Namespace.Render(writer);
                writer.WriteLine(" {");
                writer.CurrentIndent += 4;
            }

            { // Class
                Modifier.Render(writer);

                writer.Write((IsStruct ? "struct" : "class") + " ");

                writer.Write(Name);

                // Class body
                using (new BracketIndentCodeContext(writer, Indent)) {

                    foreach (var child in this.ElementChildren) {
                        child.Render(writer);
                        writer.WriteLine();
                    }
                }

            }
            if (Namespace != null) {
                writer.CurrentIndent -= 4;
                writer.WriteLine();
                writer.Write("} // namespace ");
                Namespace.Render(writer);
                writer.WriteLine();

            }
        }

        public override INamespace? Namespace { get; }
    }
}