using System;
using System.IO;
using GCore.Source.Attributes;
using GCore.Source.Extensions;
using GCore.Source.Generators.Attributes;
using GCore.Source.Generators.Elements;

namespace GCore.Source.Generators.Razor
{
    [TaggedElement("Razor")]
    public class RazorElement : ScriptableElement, ITaggedElement
    {
        public RazorElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        protected override void RenderScript(CodeWriter writer)
        {
            var code = ReadInput();
            var model = (object?)GetModel();

            // Compile the template into an in-memory assembly
            var template = MiniRazor.Razor.Compile(code);

            // Render the template
            template.RenderAsync((TextWriter)writer, new Globals(model, this, writer));
        }
    }
}