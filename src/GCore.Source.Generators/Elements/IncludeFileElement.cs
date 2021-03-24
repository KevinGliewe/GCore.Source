using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GCore.Source.Attributes;
using GCore.Source.Extensions;
using GCore.Source.Generators.Attributes;

namespace GCore.Source.Generators.Elements
{
    [TaggedElement("IncludeFile")]
    public class IncludeFileElement : TaggedElement, IRawElement
    {
        [Config("FilePath")] public string FilePath { get; set; } = "";

        public string[] Lines { get; protected set; } = new string[0];

        public IncludeFileElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        public void SetLines(IEnumerable<string> lines)
        {
            Lines = lines.ToArray();
        }

        public void SetLines(string lines)
        {
            SetLines(lines.SplitNewLine());
        }

        public override void Configure(IReadOnlyDictionary<string, string> config)
        {
            base.Configure(config);

            if(!File.Exists(FilePath))
                throw new Exception("FilePath does not extist " + FilePath);

            SetLines(File.ReadAllLines(FilePath));
        }

        public override void Render(CodeWriter writer)
        {
            if (!(_startLine is null) && RenderTags)
                writer.WriteLine(_startLine);

            for (int i = 0; i < Lines.Length; i++) {
                writer.Write(Lines[i]);
                if (i < Lines.Length - 1)
                    writer.WriteLine();
            }

            if (Lines.Length > 0)
                writer.WriteLine();

            if (!(_stopLine is null) && RenderTags)
                writer.Write(_stopLine);
        }
    }
}