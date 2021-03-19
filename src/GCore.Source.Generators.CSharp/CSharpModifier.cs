using System;
using System.Collections.Generic;
using System.Text;

namespace GCore.Source.Generators.CSharp
{
    [Flags]
    public enum CSharpModifier
    {
        None = 0,
        Public = 1 << 0,
        Private = 1 << 1,
        Internal = 1 << 2,
        Protected = 1 << 3,
        Abstract = 1 << 4,
        Static = 1 << 5,
        Partial = 1 << 6,
    }

    public static class CSharpModifierExtensions
    {
        public static void Render(this CSharpModifier val, CodeWriter writer)
        {
            if (val.HasFlag(CSharpModifier.Public)) writer.Write("public ");
            if (val.HasFlag(CSharpModifier.Private)) writer.Write("private ");
            if (val.HasFlag(CSharpModifier.Internal)) writer.Write("internal ");
            if (val.HasFlag(CSharpModifier.Protected)) writer.Write("protected ");
            if (val.HasFlag(CSharpModifier.Abstract)) writer.Write("abstract ");
            if (val.HasFlag(CSharpModifier.Static)) writer.Write("static ");
            if (val.HasFlag(CSharpModifier.Partial)) writer.Write("partial ");
        }
    }
}
