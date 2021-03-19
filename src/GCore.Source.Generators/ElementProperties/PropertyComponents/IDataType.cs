namespace GCore.Source.Generators.ElementProperties.PropertyComponents
{
    public interface IDataType<TNamespace> : 
        IRenderable
        where TNamespace : class, INamespace
    {
        TNamespace? Namespace { get; }
        string Name { get; }
    }
}