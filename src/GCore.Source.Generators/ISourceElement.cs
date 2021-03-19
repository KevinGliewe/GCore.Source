using System;
using System.Collections.Generic;
using System.Text;

using GCore.Data.Structure.InheritedTree;

namespace GCore.Source.Generators
{
    public interface ISourceElement<TTree, TNode, TProps> : 
        INode<TTree, TNode, string, TProps>, IRenderable
        where TTree : ISourceTree<TTree, TNode, TProps>
        where TNode : ISourceElement<TTree, TNode, TProps>
        where TProps : ISourceElementProperty
    {
        object? OModel { get; set; }

        void InitElement();
    }
}
