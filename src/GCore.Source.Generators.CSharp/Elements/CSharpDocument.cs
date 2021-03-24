using System.Collections.Generic;
using GCore.Source.Generators.CSharp.Properties;

namespace GCore.Source.Generators.CSharp.Elements
{
    public class CSharpDocument : CSharpElement
    {
        public IList<CSharpNamespace> Usings { get; } = new List<CSharpNamespace>()
        {
            new CSharpNamespace("System"),
            new CSharpNamespace("System", "Linq"),
        };
        public CSharpDocument(SourceElement? parent, string name) : base(parent, name)
        {
            Indent = 0;
        }

        public override void Render(CodeWriter writer)
        {
            foreach (var @using in Usings) {
                writer.Write("using ");
                @using.Render(writer);
                writer.WriteLine(";");
            }

            base.Render(writer);
        }
    }
}