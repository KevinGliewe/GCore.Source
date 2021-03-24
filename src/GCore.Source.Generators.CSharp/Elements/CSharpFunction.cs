using System.Linq;
using GCore.Source.Attributes;
using GCore.Source.CodeContexts;
using GCore.Source.Generators.Elements;
using GCore.Source.Generators.Elements.Components;

namespace GCore.Source.Generators.CSharp.Elements
{
    public class CSharpFunction : FunctionElement, ICSharpModifiable
    {
        [Config("Modifier")]
        public CSharpModifier Modifier { get; set; }

        public override DataType? ReturnType { get; }


        public CSharpFunction(SourceElement? parent, string name, DataType? returnType = null, CSharpModifier modifier = CSharpModifier.None) : base(parent, name)
        {
            ReturnType = returnType;
            Modifier = modifier;
        }

        public override void Render(CodeWriter writer) {
            Modifier.Render(writer);
            if (Modifier != CSharpModifier.None)
                writer.Write(' ');

            ReturnType?.Render(writer);

            writer.Write(' ');
            writer.Write(Name);

            // Arguments
            using (new BracketCodeContext(writer, BracketType.Round)) {
                var arguments = this.GetElementsLocally<VariableElement>().Where(v => v.IsArgument).ToArray();
                for (int i = 0; i < arguments.Length; i++) {
                    arguments[i].Render(writer);
                    if (i < arguments.Length - 1)
                        writer.Write(", ");
                }

            }

            if (Modifier.HasFlag(CSharpModifier.Abstract))
                writer.WriteLine(';');
            else
                using (new BracketIndentCodeContext(writer)) {
                    RenderBody(writer);
                }
        }
        protected virtual void RenderBody(CodeWriter writer)
        {
            writer.WriteLine("// Body not defined!");
        }

    }
}