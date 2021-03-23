using System.CodeDom.Compiler;

namespace GCore.Source
{
    public interface IRenderable
    {
        void Render(CodeWriter writer);
    }
}