using System.CodeDom.Compiler;

namespace GCore.Source.Generators
{
    public interface IRenderable
    {
        void Render(IndentedTextWriter writer);
    }
}