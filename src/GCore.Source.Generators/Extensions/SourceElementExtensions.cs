namespace GCore.Source.Generators.Extensions
{
    public static class SourceElementExtensions
    {
        public static SourceElement Add(this SourceElement @this, SourceElement element)
        {
            @this.ElementChildren.Add(element);
            return element;
        }
    }
}