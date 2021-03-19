using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GCore.Source.CodeContexts;
using GCore.Source.Generators.CSharp.Properties;
using GCore.Source.Generators.CSharp.Properties.Variables.Components;

namespace GCore.Source.Generators.CSharp.Elements
{
    public class CSharpClass : CSharpElement
    {
        public IList<CSharpNamespace> Usings { get; } = new List<CSharpNamespace>()
        {
            new CSharpGenericNamespace("System"),
            new CSharpGenericNamespace("System", "Linq"),
        };

        public override void Render(CodeWriter writer)
        {
            foreach (var @using in Usings)
            {
                writer.Write("using ");
                @using.Render(writer);
                writer.WriteLine(";");
            }

            if (Namespace != null)
            {
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
                using (new BracketIndentCodeContext(writer))
                {
                    // Render variables
                    foreach (var variable in this.SelfPropertys.Select(p => p.Value).OfType<CSharpVariable>()) {
                        variable.Render(writer);
                        writer.WriteLine(";");
                    }

                    foreach (var child in this.Children)
                    {
                        child.Render(writer);
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

        public override void InitElement()
        {
        }

        public CSharpNamespace? Namespace { get; set; }

        public bool IsStruct = false;
    }
}