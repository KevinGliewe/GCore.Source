using System.Reflection;
using GCore.Source.Generators.Attributes;
using GCore.Source.Generators.Elements;
using GCore.Source.Scripting;

namespace GCore.Source.Generators.Scripting
{
    [TaggedElement("T42")]
    public class T42Element : ScriptableElement, ITaggedElement
    {
        public T42Element(SourceElement? parent, string name) : base(parent, name)
        {
        }

        protected override void RenderScript(CodeWriter writer)
        {
            var code = T42Template.T42ToScript(ReadInput());
            var model = GetModel();

            var result = new ScriptRunner(
                new Assembly[]
                {

                    typeof(CodeWriter).Assembly,
                    typeof(SourceElement).Assembly,
                    typeof(ScriptElement).Assembly,
                    typeof(System.Dynamic.ExpandoObject).Assembly,
                    typeof(GCore.GMath.Half).Assembly
                },
                new string[]
                {
                    "System",
                    "System.Dynamic",
                    "GCore.Source"
                }
            ).Run(code, new Globals(model, this, writer)).Result;

            if (result is EvaluationResult.Error err)
                throw err.Exception;
        }
    }
}