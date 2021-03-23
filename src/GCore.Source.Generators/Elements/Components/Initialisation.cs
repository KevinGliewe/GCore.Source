using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace GCore.Source.Generators.Elements.Components
{
    public class Initialisation :
        IInitialisation
    {
        public object InitValue { get; set; }

        public Initialisation(object initValue)
        {
            InitValue = initValue;
        }
        public virtual void Render(CodeWriter writer) => writer.Write(InitValue.ToString());

    }
}
