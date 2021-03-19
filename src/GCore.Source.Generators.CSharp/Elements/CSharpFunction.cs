using System.Collections.Generic;
using System.Linq;
using GCore.Source.CodeContexts;
using GCore.Source.Generators.CSharp.Properties;
using GCore.Source.Generators.CSharp.Properties.Variables;

namespace GCore.Source.Generators.CSharp.Elements
{
    public class CSharpFunction : CSharpElement
    {

        public static readonly string KEY_RETURNTYPE = "return";

        public override void Render(CodeWriter writer)
        {
            Modifier.Render(writer);
            if (Modifier != CSharpModifier.None)
                writer.Write(' ');

            if(Defines(KEY_RETURNTYPE))
                this.Get(KEY_RETURNTYPE).Render(writer);
            else
                writer.Write("void");

            writer.Write(' ');
            writer.Write(Name);

            // Arguments
            using (new BracketCodeContext(writer, BracketType.Round))
            {
                var arguments = this.SelfPropertys.OfType<CSharpFunctionArgument>().ToArray();
                for (int i = 0; i < arguments.Length; i++)
                {
                    arguments[i].Render(writer);
                    if (i < arguments.Length - 1)
                        writer.Write(", ");
                }

            }

            if (Modifier.HasFlag(CSharpModifier.Abstract))
                writer.WriteLine(';');
            else
                using (new BracketIndentCodeContext(writer))
                {
                    RenderBody(writer);
                }
        }

        public override void InitElement()
        {
        }

        protected virtual void RenderBody(CodeWriter writer)
        {
            writer.WriteLine("// Body not defined!");
        }
    }
}