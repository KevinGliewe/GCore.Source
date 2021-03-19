using System;
using System.Collections.Generic;
using System.Text;

namespace GCore.Source.Generators.CSharp.Properties.Variables
{
    public class CSharpReturnType : CSharpType
    {
        public CSharpType Type { get; set; }

        public CSharpReturnType(CSharpType type) : base(type.Name, type.Namespace)
        {
            Type = type;
        }

        public override void Render(CodeWriter writer)
        {
            Type.Render(writer);
        }
    }
}
