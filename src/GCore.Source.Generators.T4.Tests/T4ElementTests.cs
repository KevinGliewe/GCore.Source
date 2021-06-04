using GCore.Source.Extensions;
using GCore.Source.Generators.Extensions;
using NUnit.Framework;

namespace GCore.Source.Generators.T4.Tests
{
    public class T4ElementTests
    {
        [SetUp]
        public void Setup() {
            // Need to reference this Type, so the Assembly loads into memory
            typeof(T4Element).GetType();
        }

        [Test]
        public void Simple() {
            var se = "<[T4 Dst=\".\" RenderTags=\"false\"]>\nHello\n<[/T4]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual("Hello", result);
        }

        [Test]
        public void ScriptFileSimple() {
            var se = "<[T4 Dst=\".\" ScriptFile=\"TestData/TemplateSimple.t4\" RenderTags=\"false\"]>\n<[/T4]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual("Hello World", result);
        }

        [Test]
        public void ScriptFileCount() {
            var se = "<[T4 Dst=\".\" ScriptFile=\"TestData/TemplateCount.t4\" RenderTags=\"false\"]>\n<[/T4]>".ParseToSourceElement();

            var result = se.Render().Trim().SplitNewLine();

            for (int i = 0; i < 42; i++)
            {
                Assert.AreEqual($"{i} = {i*i}", result[i]);
            }
        }
    }
}