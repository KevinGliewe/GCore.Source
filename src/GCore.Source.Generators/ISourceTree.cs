using GCore.Data.Structure.InheritedTree;

namespace GCore.Source.Generators
{
    public interface ISourceTree<TTree, TNode, TProps> :
        ITree<TTree, TNode, string, TProps>, IRenderable
        where TTree : ISourceTree<TTree, TNode, TProps>
        where TNode : ISourceElement<TTree, TNode, TProps>
        where TProps : ISourceElementProperty
    {
        
    }
}