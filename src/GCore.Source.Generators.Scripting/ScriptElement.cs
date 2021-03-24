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
using Newtonsoft.Json.Linq;

namespace GCore.Source.Generators.Scripting
{
    [TaggedElement("Script")]
    public class ScriptElement : TaggedElement, ITaggedElement
    {
        public class Globals
        {
            public dynamic? Model;
            public ScriptElement Element;
            public CodeWriter Writer;

            public Globals(dynamic? model, ScriptElement element, CodeWriter writer)
            {
                Model = model;
                Element = element;
                Writer = writer;
            }
        }

        [Config("ModelFile")]
        public string? ModelFile { get; set; }

        [Config("ScriptFile")]
        public string? ScriptFile { get; set; }

        [Config("Src")] 
        public string? Src { get; set; } = ".";

        [Config("Dst")]
        public string? Dst { get; set; }

        public ScriptElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        public override void Render(CodeWriter writer)
        {


            var code = ScriptFile is null ? ReadScriptElement() : ReadScriptFile();
            var model = GetModel();

            var cw = new CodeWriter();
            var result = CSharpScript.RunAsync(code, BuildScriptOptions(), globals: new Globals(model, this, cw)).Result;
            WriteOutput(cw.GenerateCode());


            base.Render(writer);
        }

        string ReadScriptFile()
        {
            if(!File.Exists(ScriptFile))
                throw new Exception("Script file not found " + ScriptFile);

            return File.ReadAllText(ScriptFile);
        }

        string ReadScriptElement()
        {
            if(Src is null)
                throw new Exception("Src is null");

            var srcElem = GetElement(Src.Split(PATH_SEP));

            if(srcElem is null)
                throw new Exception($"{string.Join(PATH_SEP, GetPath())} does not have a child named {Src}");

            if (srcElem is IRawElement srcRaw)
            {
                return string.Join("\n", srcRaw.Lines);
            } 
            else if (srcElem == this)
            {
                var cw = new CodeWriter();
                for (int i = 0; i < ElementChildren.Count; i++) {
                    ElementChildren[i].Render(cw);
                    if (i < ElementChildren.Count - 1)
                        cw.WriteLine();
                }
                return cw.GenerateCode();
            }
            else
            {
                var cw = new CodeWriter();
                srcElem.Render(cw);
                return cw.GenerateCode();
            }
        }

        void WriteOutput(string result)
        {
            var dst = Dst ?? throw new Exception(this + " output element 'Dst' not defined!");

            SourceElement dstElem = GetElement(dst.Split(PATH_SEP)) ??
                                    throw new Exception(this + " output element 'Dst' not found!");

            if (dstElem == this)
            {
                this.ElementChildren.Clear();
                this.ElementChildren.Add(new RawElement(this, "ScriptResult", result));
            }
            else
            {
                var dstRaw = dstElem as IRawElement ??
                             throw new Exception(dstElem + $" is a {dstElem.GetType()} but should be IRawElement");

                dstRaw.SetLines(result.SplitNewLine());
            }
        }

        dynamic? GetModel()
        {
            if (ModelFile is null)
                return null;
            return JObject.Parse(File.ReadAllText(ModelFile));
        }

        protected virtual ScriptOptions BuildScriptOptions(Assembly[]? assemblys = null, string[]? imports = null) {
            var so = ScriptOptions.Default
                .AddReferences(
                    typeof(CodeWriter).Assembly,
                    typeof(SourceElement).Assembly,
                    typeof(ScriptElement).Assembly,
                    typeof(System.Dynamic.ExpandoObject).Assembly,
                    typeof(Newtonsoft.Json.JsonConvert).Assembly
                )
                .AddImports(
                    "System",
                    "System.Dynamic",
                    "Newtonsoft.Json",
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