using GCore.Data.Structure.InheritedTree;

namespace GCore.Source.Generators
{
    public interface ISourceTree<TImpl, TProps> :
        ITree<TImpl, string, TProps>, IRenderable
        where TImpl : INode<TImpl, string, TProps>
        where TProps : ISourceElementProperty
    {
        
    }
}