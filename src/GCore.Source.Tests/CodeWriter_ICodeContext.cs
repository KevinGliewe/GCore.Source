using System;
using System.IO;
using GCore.Source.CodeContexts;
using NUnit.Framework;

namespace GCore.Source.Tests
{
    public class CodeWriter_ICodeContext
    {
        private Func<Action<CodeWriter>?, CodeWriter> _textWriter = (conf) =>
        {
            var cw = new CodeWriter()
            {
                NewLine = "\r\n"
            };
            conf?.Invoke(cw);
            return cw;
        };

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Bracket()
        {
            var cw = _textWriter(null);

            using (new BracketCodeContext(cw))
            {
                cw.Write("Test");
                Assert.IsNotNull(cw.CurrentContext);
            }

            Assert.AreEqual(cw.GenerateCode(), "{Test}");
            Assert.IsNull(cw.CurrentContext);
        }

        [Test]
        public void BracketIndent()
        {
            var cw = _textWriter(null);

            using (new BracketIndentCodeContext(cw))
                cw.Write("Test");

            Assert.AreEqual(cw.GenerateCode(), "{\r\n    Test\r\n}");
            Assert.IsNull(cw.CurrentContext);
        }
    }
}