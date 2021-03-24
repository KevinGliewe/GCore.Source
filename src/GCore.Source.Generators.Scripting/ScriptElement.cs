using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using GCore.Source.Attributes;
using GCore.Source.Extensions;
using GCore.Source.Generators.Attributes;
using GCore.Source.Generators.Elements;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

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

            var result = CSharpScript.RunAsync(code, BuildScriptOptions(), globals: new Globals(model, this, writer)).Result;
        }


        protected virtual ScriptOptions BuildScriptOptions(Assembly[]? assemblys = null, string[]? imports = null) {
            var so = ScriptOptions.Default
                .AddReferences(
                    typeof(CodeWriter).Assembly,
                    typeof(SourceElement).Assembly,
                    typeof(ScriptElement).Assembly,
                    typeof(System.Dynamic.ExpandoObject).Assembly
                )
                .AddImports(
                    "System",
                    "System.Dynamic",
                    "GCore.Source"
                );

            if (assemblys != null)
                so.AddReferences(assemblys);

            if (imports != null)
                so.AddImports(imports);

            return so;
        }
    }
}