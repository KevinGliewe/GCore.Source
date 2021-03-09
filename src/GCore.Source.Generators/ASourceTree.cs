using System.CodeDom.Compiler;
using GCore.Data.Structure.InheritedTree;

namespace GCore.Source.Generators
{
    public abstract class ASourceTree<TImpl, TProps> :
        Tree<TImpl, string, TProps>, ISourceTree<TImpl, TProps>
        where TImpl : INode<TImpl, string, TProps>
        where TProps : ISourceElementProperty
    {
        protected ASourceTree(string root, string separator = ":") : base(root, separator)
        {
        }

        protected ASourceTree(RawNode<TImpl, string, TProps> rawNode, string separator = ":") : base(rawNode, separator)
        {
        }

        public abstract void Render(IndentedTextWriter writer);
    }
}