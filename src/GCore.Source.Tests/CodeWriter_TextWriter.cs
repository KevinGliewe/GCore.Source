using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using GCore.Source;

namespace GCore.Source.Tests
{
    public class CodeWriter_TextWriter
    {
        private Func<Action<CodeWriter>?, TextWriter> _textWriter = (conf) =>
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
        public void WriteLine()
        {
            var tw = _textWriter(null);

            var writeText = "Test";

            tw.WriteLine(writeText);

            if (tw is CodeWriter cw)
            {
                Assert.AreEqual(cw.GenerateCode(), writeText + tw.NewLine);
                Assert.AreEqual(cw.Location.AbsoluteIndex, writeText.Length + tw.NewLine.Length);
                Assert.AreEqual(cw.Location.CharacterIndex, 0);
                Assert.AreEqual(cw.Location.LineIndex, 1);
            }
            else
            {
                Assert.Fail("tw is not castable to CodeWriter");
            }
        }

        [Test]
        public void WriteLine_Indented()
        {
            var tw = _textWriter(cw => cw.CurrentIndent = 1);

            var writeText = "Test";

            tw.WriteLine(writeText);

            if (tw is CodeWriter cw)
            {
                Assert.AreEqual(" " + writeText + tw.NewLine, cw.GenerateCode());
                Assert.AreEqual(1 + writeText.Length + tw.NewLine.Length, cw.Location.AbsoluteIndex);
                Assert.AreEqual(0, cw.Location.CharacterIndex);
                Assert.AreEqual(1, cw.Location.LineIndex);
            }
            else
            {
                Assert.Fail("tw is not castable to CodeWriter");
            }
        }

        [Test]
        public void WriteLine_NegativeIndented1()
        {
            var tw = _textWriter(cw => cw.CurrentIndent = -1);

            var nl = tw.NewLine;

            var writeText = $"1{nl} 2{nl}  3";
            var expectText = $"1{nl}2{nl} 3";

            tw.Write(writeText);

            if (tw is CodeWriter cw)
            {
                Assert.AreEqual(expectText, cw.GenerateCode());
            }
            else
            {
                Assert.Fail("tw is not castable to CodeWriter");
            }
        }

        [Test]
        public void WriteLine_NegativeIndented2()
        {
            var tw = _textWriter(cw => cw.CurrentIndent = -1);

            var nl = tw.NewLine;

            var writeText = $"  1{nl} 2{nl}3";
            var expectText = $" 1{nl}2{nl}3";

            tw.Write(writeText);

            if (tw is CodeWriter cw)
            {
                Assert.AreEqual(expectText, cw.GenerateCode());
            }
            else
            {
                Assert.Fail("tw is not castable to CodeWriter");
            }

        }
    }
}