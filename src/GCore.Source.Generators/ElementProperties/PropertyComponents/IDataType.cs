namespace GCore.Source.Generators.ElementProperties.PropertyComponents
{
    public interface IDataType<TNamespace> : 
        IRenderable
        where TNamespace : INamespace
    {
        TNamespace Namespace { get; }
        string Name { get; }
    }
}