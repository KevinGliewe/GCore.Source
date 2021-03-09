using System.CodeDom.Compiler;
using GCore.Data.Structure.InheritedTree;

namespace GCore.Source.Generators
{
    public abstract class ASourceElement<TImpl, TProps> :
        Node<TImpl, string, TProps>, ISourceElement<TImpl, TProps>
        where TImpl : Node<TImpl, string, TProps>
        where TProps : ISourceElementProperty
    {
        public abstract void Render(IndentedTextWriter writer);
        public abstract void InitElement();

        public object OModel { get; set; }
    }
}