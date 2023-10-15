using GCore.Source.Extensions;
using GCore.Source.Generators.Elements;
using GCore.Source.Generators.Extensions;
using GCore.Source.Helper;
using NUnit.Framework;

namespace GCore.Source.Generators.Tests
{
    public class RawAlignedTests
    {
        [Test]
        public void SplitLines()
        {
            var root = new RawAlignedElement(null, "Root");

            root.Add(new RawElement(root, "1", "§Line1", "§§Line2"));
            root.Add(new RawElement(root, "2", "§§§Line3\r\n§§§§Line4\n§§§§§Line5"));

            var result = root.Render().SplitNewLine();

            Assert.AreEqual(5, result.Length);

            for (int i = 1; i <= 5; i++)
                Assert.AreEqual(StringHelper.GetSpaces(i * 4) + "Line" + i, result[i-1].TrimEnd());
        }
    }
}