using System.Collections.Generic;
using GCore.Source.Extensions;

namespace GCore.Source.Generators.Elements
{
    public interface IRawElement : IRenderable
    {
        public string[] Lines { get; }

        void SetLines(IEnumerable<string> lines);

        void SetLines(string lines);
    }
}