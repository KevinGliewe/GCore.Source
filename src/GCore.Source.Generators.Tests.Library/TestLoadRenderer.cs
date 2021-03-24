using GCore.Source.Generators.Elements;

namespace GCore.Source.Generators.Tests.Library
{
    public class TestLoadRenderer : IRenderObject
    {
        public void Render(CodeWriter writer, SourceElement element, object? model)
        {
            writer.WriteLine(this.GetType().FullName);
        }
    }
}