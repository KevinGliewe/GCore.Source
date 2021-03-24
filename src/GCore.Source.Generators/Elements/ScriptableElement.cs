using System;
using System.IO;
using GCore.Source.Attributes;
using GCore.Source.Extensions;
using GCore.Source.Generators.Attributes;
using GCore.Source.Helper;

namespace GCore.Source.Generators.Elements
{
    [TaggedElement("")]
    public abstract class ScriptableElement : TaggedElement
    {

        public class Globals
        {
            public dynamic? Model;
            public ScriptableElement Element;
            public CodeWriter Writer;

            public Globals(dynamic? model, ScriptableElement element, CodeWriter writer) 
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

        protected ScriptableElement(SourceElement? parent, string name) : base(parent, name) {
        }

        public override void Render(CodeWriter writer) 
        {

            var cw = new CodeWriter();

            RenderScript(cw);

            WriteOutput(cw.GenerateCode());


            base.Render(writer);
        }

        string ReadScriptFile()
        {
            if (!File.Exists(ScriptFile))
                throw new Exception("Script file not found " + ScriptFile);

            return File.ReadAllText(ScriptFile);
        }

        string ReadScriptElement() 
        {
            if (Src is null)
                throw new Exception("Src is null");

            var srcElem = GetElement(Src.Split(PATH_SEP));

            if (srcElem is null)
                throw new Exception($"{string.Join(PATH_SEP, GetPath())} does not have a child named {Src}");

            if (srcElem is IRawElement srcRaw) {
                return string.Join("\n", srcRaw.Lines);
            } else if (srcElem == this) {
                var cw = new CodeWriter();
                for (int i = 0; i < ElementChildren.Count; i++) {
                    ElementChildren[i].Render(cw);
                    if (i < ElementChildren.Count - 1)
                        cw.WriteLine();
                }
                return cw.GenerateCode();
            } else {
                var cw = new CodeWriter();
                srcElem.Render(cw);
                return cw.GenerateCode();
            }
        }

        protected string ReadInput()
        {
            return ScriptFile is null ? ReadScriptElement() : ReadScriptFile();
        }

        protected void WriteOutput(string result) {
            var dst = Dst ?? throw new Exception(this + " output element 'Dst' not defined!");

            SourceElement dstElem = GetElement(dst.Split(PATH_SEP)) ??
                                    throw new Exception(this + " output element 'Dst' not found!");

            if (dstElem == this) {
                this.ElementChildren.Clear();
                this.ElementChildren.Add(new RawElement(this, "ScriptResult", result));
            } else {
                var dstRaw = dstElem as IRawElement ??
                             throw new Exception(dstElem + $" is a {dstElem.GetType()} but should be IRawElement");

                dstRaw.SetLines(result.SplitNewLine());
            }
        }

        protected dynamic? GetModel()
        {
            if (ModelFile is null)
                return null;
            return JsonDynamic.QueryFile(ModelFile);
        }

        protected abstract void RenderScript(CodeWriter writer);

    }
    
}