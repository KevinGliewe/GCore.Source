using System;
using System.CodeDom.Compiler;
using GCore.Data.Structure.InheritedTree;

namespace GCore.Source.Generators
{
    public abstract class ASourceTree<TTree, TNode, TProps> :
        Tree<TTree, TNode, string, TProps>, ISourceTree<TTree, TNode, TProps>
        where TTree : ASourceTree<TTree, TNode, TProps>
        where TNode : class, ISourceElement<TTree, TNode, TProps>
        where TProps : class, ISourceElementProperty
    {
        protected ASourceTree(TNode rootNode, string rootName = "root", string separator = ":")
            : base(rootNode, rootName, separator)
        {
        }

        protected ASourceTree(string root, string separator = ":") : base(root, separator)
        {
        }

        protected ASourceTree(RawNode<TNode, string, TProps> rawNode, string separator = ":") : base(rawNode, separator)
        {
        }

        public abstract void Render(CodeWriter writer);
    }
}