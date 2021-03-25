using GCore.Source.Cli.Tests.Extensions;
using GCore.Source.Generators.Extensions;
using NUnit.Framework;

namespace GCore.Source.Generators.Razor.Testing
{
    public class RazorElementTests
    {
        [SetUp]
        public void Setup() {
            // Need to reference this Type, so the Assembly loads into memory
            typeof(RazorElement).GetType();
        }

        [Test]
        public void Simple() {
            var se = "<[Razor Dst=\".\" RenderTags=\"false\"]>\nHello\n<[/Razor]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual("Hello", result);
        }

        [Test]
        public void ScriptFile() {
            var se = "<[Razor Dst=\".\" ScriptFile=\"TestData/TemplateSimple.razor\" RenderTags=\"false\"]>\n<[/Razor]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual("Hello World", result);
        }

        [Test]
        public void ScriptModelFile() {
            var se = "<[Razor Dst=\".\" ScriptFile=\"TestData/TemplateModel.razor\" ModelFile=\"TestData/Model.json\" RenderTags=\"false\"]>\n<[/Razor]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual("42", result);
        }

        [Test]
        public void DstSrc() {
            var se = @"
<[Razor Dst=""..:Dest"" Src=""..:Source""]>
<[/Razor]>
/*<[Raw Name=""Source""]>
Hello World
<[/Raw]>*/
<[Raw Name=""Dest""]>
Will be overwritten
<[/Raw]>
            ".Trim().ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual(@"
<[Razor Dst=""..:Dest"" Src=""..:Source""]>
<[/Razor]>
/*<[Raw Name=""Source""]>
Hello World
<[/Raw]>*/
<[Raw Name=""Dest""]>
Hello World
<[/Raw]>".Trim().FixNL(), result);
        }
    }
}