using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace GCore.Source.Generators.ElementProperties.PropertyComponents
{
    public abstract class AInitialisation :
        IInitialisation
    {
        public abstract void Render(CodeWriter writer);

        public object? InitValue { get; set; }
    }
}
