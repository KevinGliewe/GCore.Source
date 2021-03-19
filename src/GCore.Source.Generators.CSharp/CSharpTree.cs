using GCore.Data.Structure.InheritedTree;

namespace GCore.Source.Generators.CSharp
{
    public class CSharpTree : ASourceTree<CSharpTree, CSharpElement, ICSharpProperty>
    {
        public CSharpTree() : this(new CSharpProject(), "root") { }

        public CSharpTree(CSharpElement rootNode, string rootName = "root", string separator = ":") 
            : base(rootNode, rootName, separator)
        {
        }

        public CSharpTree(string root, string separator = ":") 
            : base(root, separator)
        {
        }

        public CSharpTree(RawNode<CSharpElement, string, ICSharpProperty> rawNode, string separator = ":") 
            : base(rawNode, separator)
        {
        }

        public override void Render(CodeWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}