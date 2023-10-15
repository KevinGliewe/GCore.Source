using GCore.Source.Helper;
using NUnit.Framework;

namespace GCore.Source.Tests
{
    public class StringAlign
    {
        private static string[] TestString = new string[] { "|2|", "1||3" };

        [Test]
        public void Basic4()
        {
            var aligned = StringHelper.Align(TestString, '|');
            //                      123456789
            Assert.AreEqual("    2", aligned[0]);
            Assert.AreEqual("1       3", aligned[1]);
        }

        [Test]
        public void Basic1()
        {
            var aligned = StringHelper.Align(TestString, '|', 1);
            //                      123456789
            Assert.AreEqual("  2", aligned[0]);
            Assert.AreEqual("1   3", aligned[1]);
        }
    }
}