using System.Collections.Generic;
using GCore.Source.Extensions;
using NUnit.Framework;

namespace GCore.Source.Tests
{
    public class Extensions_String
    {
        [SetUp]
        public void Setup() {
        }

        [TestCase("Line1\r\nLine2")]
        [TestCase("Line1\rLine2")]
        [TestCase("Line1\nLine2")]
        public void SplitNewLines(string splitable)
        {
            var lines = splitable.SplitNewLine();

            Assert.AreEqual(2, lines.Length);
            Assert.AreEqual(lines[0], "Line1");
            Assert.AreEqual(lines[1], "Line2");
        }

        [Test]
        public void ExtractParameters()
        {
            var param = @"test1=""val1"" test2="" \r\n\""""".ExtractParameters();

            Assert.AreEqual(2, param.Count);

            Assert.IsTrue(param.ContainsKey("test1"));
            Assert.IsTrue(param.ContainsKey("test2"));

            Assert.AreEqual("val1", param["test1"]);
            Assert.AreEqual(" \\r\\n\\\"", param["test2"]);
        }
    }
}