using System;
using GCore.Source.Extensions;
using GCore.Source.Generators.Elements;
using GCore.Source.Generators.Extensions;
using NUnit.Framework;

namespace GCore.Source.Generators.Tests
{
    public class TestRenderer : IRenderObject
    {
        public void Render(CodeWriter writer, SourceElement element, object? model)
        {
            writer.WriteLine(this.GetType().FullName ?? throw new Exception());
        }
    }

    public class ObjectElementTests
    {
        [Test]
        public void FullName() {
            var se = $"<[Object Type=\"{typeof(TestRenderer).FullName}\" Dst=\".\" RenderTags=\"false\"]>\n<[/Object]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual(typeof(TestRenderer).FullName, result);
        }

        [Test]
        public void SimpleName() {
            var se = $"<[Object Type=\"{typeof(TestRenderer).Name}\" Dst=\".\" RenderTags=\"false\"]>\n<[/Object]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual(typeof(TestRenderer).FullName, result);
        }

        [Test]
        public void LoadSimpleName() {
            var se = $"<[Object Type=\"GCore.Source.Generators.Tests.Library.dll?TestLoadRenderer\" Dst=\".\" RenderTags=\"false\"]>\n<[/Object]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual("GCore.Source.Generators.Tests.Library.TestLoadRenderer", result);
        }
    }
}