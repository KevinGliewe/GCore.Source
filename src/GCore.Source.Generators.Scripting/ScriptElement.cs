using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using GCore.Source.Attributes;
using GCore.Source.Extensions;
using GCore.Source.Generators.Attributes;
using GCore.Source.Generators.Elements;
using GCore.Source.Scripting;

namespace GCore.Source.Generators.Scripting
{
    [TaggedElement("Script")]
    public class ScriptElement : ScriptableElement, ITaggedElement
    {

        public ScriptElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        protected override void RenderScript(CodeWriter writer)
        {
            var code = ReadInput();
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