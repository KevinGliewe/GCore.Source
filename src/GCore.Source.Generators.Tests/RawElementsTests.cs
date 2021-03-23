using GCore.Source.Generators.Elements;
using GCore.Source.Generators.Extensions;
using NUnit.Framework;

using GCore.Source.Extensions;
using GCore.Source.Generators.Extensions;

namespace GCore.Source.Generators.Tests
{
    public class RawElementsTests
    {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void SplitLines()
        {
            var root = new SourceElement(null, "Root");

            root.Add(new RawElement(root, "1", "Line1", "Line2"));
            root.Add(new RawElement(root, "2", "Line3\r\nLine4\nLine5"));

            var result = root.Render().SplitNewLine();

            Assert.AreEqual(5, result.Length);

            for(int i = 0; i <= 4; i++)
                Assert.AreEqual("Line" + (i + 1), result[i]);
        }
    }
}