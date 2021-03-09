using System;
using System.Collections.Generic;
using System.Text;

using GCore.Data.Structure.InheritedTree;

namespace GCore.Source.Generators
{
    public interface ISourceElement<TImpl, TProps> : 
        INode<TImpl, string, TProps>, IRenderable
        where TImpl : INode<TImpl, string, TProps>
        where TProps : ISourceElementProperty
    {
        object OModel { get; set; }

        void InitElement();
    }
}
