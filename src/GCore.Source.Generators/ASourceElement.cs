using System.CodeDom.Compiler;
using GCore.Data.Structure.InheritedTree;

namespace GCore.Source.Generators
{
    public abstract class ASourceElement<TTree, TNode, TProps> :
        Node<TTree, TNode, string, TProps>, ISourceElement<TTree, TNode, TProps>
        where TTree : class, ISourceTree<TTree, TNode, TProps>
        where TNode : ASourceElement<TTree, TNode, TProps>
        where TProps : class, ISourceElementProperty
    {
        public abstract void Render(CodeWriter writer);
        public abstract void InitElement();

        public object? OModel { get; set; }
    }
}