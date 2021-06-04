using System;
using System.Collections.Generic;
using System.Text;
using GCore.Source.Generators.Attributes;
using GCore.Source.Generators.Elements;
using Mono.TextTemplating;

namespace GCore.Source.Generators.T4
{
    [TaggedElement("T4")]
    public class T4Element : ScriptableElement, ITaggedElement
    {
        public T4Element(SourceElement? parent, string name) : base(parent, name)
        {
        }

        protected override void RenderScript(CodeWriter writer)
        {
            var code = ReadInput();
            var model = (object?)GetModel();

            var engine = new TemplatingEngine();
            var generator = new TemplateGenerator();

            /*generator.AddParameter(null, null, "", "");


            var session = generator.GetOrCreateSession();
            session.*/

            using (var compiled = engine.CompileTemplate(code, generator))
            {
                writer.Write(compiled?.Process() ?? throw new Exception($"{nameof(compiled)} is null!"));
            }
            
        }
    }
}
