using GCore.Source.CodeContexts;
using GCore.Source.Generators.Elements;
using GCore.Source.Generators.Elements.Components;

namespace GCore.Source.Generators.CSharp.Elements
{
    public class CSharpClass : TypeElement, ICSharpModifiable
    {

        public CSharpModifier Modifier { get; }

        public bool IsStruct = false;

        public CSharpClass(SourceElement? parent, string name, INamespace? ns) : base(parent, name)
        {
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
                using (new BracketIndentCodeContext(writer)) {

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