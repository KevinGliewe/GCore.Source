namespace GCore.Source.Generators.Elements.Components
{
    public interface IDataType : 
        IRenderable, INamespaceAble
    {
        string Name { get; }
    }
}