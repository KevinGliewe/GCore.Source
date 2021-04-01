using System.Collections.Generic;
using GCore.Source.Extensions;
using GCore.Source.Generators.Elements;
using GCore.Source.Generators.Extensions;
using NUnit.Framework;

namespace GCore.Source.Generators.Tests
{
    public class StringToSourceElementsTests
    {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void OnlyRaw()
        {
            var se = "Line1\nLine2\r\nLine3".ParseToSourceElement();

            Assert.AreEqual(se.ElementChildren.Count, 1);
            Assert.IsTrue(se.ElementChildren[0] is RawElement);
            Assert.AreEqual(3, (se.ElementChildren[0] as RawElement)?.Lines.Length);

            var result = se.Render().SplitNewLine();

            for (int i = 0; i <= 2; i++)
                Assert.AreEqual("Line" + (i + 1), result[i]);
        }

        [Test]
        public void EmptyTag()
        {
            var se = "<[T]>\n<[/T]>".ParseToSourceElement();

            Assert.AreEqual(1, se.ElementChildren.Count);
            Assert.IsTrue(se.ElementChildren[0] is TaggedElement);
            Assert.AreEqual(0, se.ElementChildren[0].ElementChildren.Count);
        }

        [Test]
        public void ParameterTag()
        {
            var se = "<[T Name=\"TestName\" Val1=\"asdf\"]>\n<[/T]>".ParseToSourceElement();

            Assert.AreEqual(1, se.ElementChildren.Count);
            Assert.AreEqual("TestName", se.ElementChildren[0].Name);
            Assert.AreEqual(2, se.ElementChildren[0].Config.Count);
        }

        [Test]
        public void ParameterInjectTag() {
            var root = new SourceElement(null, "root");
            root.Configure(new Dictionary<string, string>()
            {
                {"RootDir", "riDtooR"}
            });

            var se = "<[T Name=\"$(RootDir)|TestName\" Val1=\"asdf\"]>\n<[/T]>".ParseToSourceElement(root);

            Assert.AreEqual(1, se.ElementChildren.Count);
            Assert.AreEqual("riDtooR|TestName", se.ElementChildren[0].Name);
            Assert.AreEqual(2, se.ElementChildren[0].Config.Count);
        }

        [Test]
        public void FilledTag() {
            var se = "Line1\n<[T]>\nLine2\n<[/T]>\nLine3".ParseToSourceElement();

            Assert.AreEqual(3, se.ElementChildren.Count);
            Assert.IsTrue(se.ElementChildren[1] is TaggedElement);
            Assert.AreEqual(1, se.ElementChildren[1].ElementChildren.Count);

            var result = se.Render().SplitNewLine();

            Assert.AreEqual(5, result.Length);

            Assert.AreEqual("Line1", result[0]);
            Assert.AreEqual("<[T]>", result[1]);
            Assert.AreEqual("Line2", result[2]);
            Assert.AreEqual("<[/T]>", result[3]);
            Assert.AreEqual("Line3", result[4]);
        }

        [Test]
        public void NestedTags() {
            var se = "Line1\n<[T]>\nLine2\n// <[T]>\n// <[/T]>\n<[/T]>\nLine3".ParseToSourceElement();

            Assert.AreEqual(3, se.ElementChildren.Count);
            Assert.IsTrue(se.ElementChildren[1] is TaggedElement);
            Assert.AreEqual(2, se.ElementChildren[1].ElementChildren.Count);
            Assert.IsTrue(se.ElementChildren[1].ElementChildren[1] is TaggedElement);

            var result = se.Render().SplitNewLine();

            Assert.AreEqual(7, result.Length);

            Assert.AreEqual("Line1", result[0]);
            Assert.AreEqual("<[T]>", result[1]);
            Assert.AreEqual("Line2", result[2]);
            Assert.AreEqual("// <[T]>", result[3]);
            Assert.AreEqual("// <[/T]>", result[4]);
            Assert.AreEqual("<[/T]>", result[5]);
            Assert.AreEqual("Line3", result[6]);
        }

        [Test]
        public void IncludeFile()
        {
            var se = "<[IncludeFile FilePath=\"./IncludeFile.txt\"]>\n<[/IncludeFile]>".ParseToSourceElement();

            var result = se.Render().SplitNewLine();

            Assert.AreEqual(4, result.Length);

            Assert.AreEqual("IncludeFile.txt", result[1]);
            Assert.AreEqual("Content", result[2]);
        }

        [Test]
        public void Indent()
        {
            var se = "Line1\n<[T Indent=\"1\"]>\nLine2\n// <[T]>\n// <[/T]>\n<[/T]>\nLine3".ParseToSourceElement();

            var result = se.Render().SplitNewLine();

            Assert.AreEqual(7, result.Length);

            Assert.AreEqual("Line1", result[0]);
            Assert.AreEqual("<[T Indent=\"1\"]>", result[1]);
            Assert.AreEqual(" Line2", result[2]);
            Assert.AreEqual(" // <[T]>", result[3]);
            Assert.AreEqual(" // <[/T]>", result[4]);
            Assert.AreEqual("<[/T]>", result[5]);
            Assert.AreEqual("Line3", result[6]);
        }

        [Test]
        public void NegativeIndent() {
            var se = "Line1\n<[T Indent=\"-1\"]>\n Line2\n  // <[T]>\n  // <[/T]>\n<[/T]>\nLine3".ParseToSourceElement();

            var result = se.Render().SplitNewLine();

            Assert.AreEqual(7, result.Length);

            Assert.AreEqual("Line1", result[0]);
            Assert.AreEqual("<[T Indent=\"-1\"]>", result[1]);
            Assert.AreEqual("Line2", result[2]);
            Assert.AreEqual(" // <[T]>", result[3]);
            Assert.AreEqual(" // <[/T]>", result[4]);
            Assert.AreEqual("<[/T]>", result[5]);
            Assert.AreEqual("Line3", result[6]);
        }

        [Test]
        public void AbsoluteIndent() {
            var se = @"
Line1
<[T Indent=""1""]>
Line2
// <[T AbsoluteIndent=""0""]>
Line3
// <[/T]>
<[/T]>
Line4".Trim().ParseToSourceElement();

            var result = se.Render().SplitNewLine();

            Assert.AreEqual(8, result.Length);

            int i = 0;

            Assert.AreEqual("Line1", result[i++]);
            Assert.AreEqual("<[T Indent=\"1\"]>", result[i++]);
            Assert.AreEqual(" Line2", result[i++]);
            Assert.AreEqual(" // <[T AbsoluteIndent=\"0\"]>", result[i++]);
            Assert.AreEqual("Line3", result[i++]);
            Assert.AreEqual(" // <[/T]>", result[i++]);
            Assert.AreEqual("<[/T]>", result[i++]);
            Assert.AreEqual("Line4", result[i++]);
        }

        [Test]
        public void AbsoluteNegativeIndent() {
            var se = @"
Line1
<[T Indent=""1""]>
Line2
// <[T AbsoluteIndent=""-1""]>
  Line3
// <[/T]>
<[/T]>
Line4".Trim().ParseToSourceElement();

            var result = se.Render().SplitNewLine();

            Assert.AreEqual(8, result.Length);

            int i = 0;

            Assert.AreEqual("Line1", result[i++]);
            Assert.AreEqual("<[T Indent=\"1\"]>", result[i++]);
            Assert.AreEqual(" Line2", result[i++]);
            Assert.AreEqual(" // <[T AbsoluteIndent=\"-1\"]>", result[i++]);
            Assert.AreEqual(" Line3", result[i++]);
            Assert.AreEqual(" // <[/T]>", result[i++]);
            Assert.AreEqual("<[/T]>", result[i++]);
            Assert.AreEqual("Line4", result[i++]);
        }
    }
}