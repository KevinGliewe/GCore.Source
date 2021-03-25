using GCore.Source.Cli.Tests.Extensions;
using GCore.Source.Generators.Extensions;
using NUnit.Framework;

namespace GCore.Source.Generators.Scripting.Tests
{
    public class ScriptElementTests
    {
        [SetUp]
        public void Setup()
        {
            // Need to reference this Type, so the Assembly loads into memory
            typeof(ScriptElement).GetType();
        }

        [Test]
        public void Simple() {
            var se = "<[Script Dst=\".\" RenderTags=\"false\"]>\nWriter.Write(\"Hello\");\n<[/Script]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual("Hello", result);
        }

        [Test]
        public void ScriptFile()
        {
            var se = "<[Script Dst=\".\" ScriptFile=\"TestData/ScriptSimple.csx\" RenderTags=\"false\"]>\n<[/Script]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual("Hello World", result);
        }

        [Test]
        public void ScriptModelFile() {
            var se = "<[Script Dst=\".\" ScriptFile=\"TestData/ScriptModel.csx\" ModelFile=\"TestData/Model.json\" RenderTags=\"false\"]>\n<[/Script]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual("42", result);
        }

        [Test]
        public void DstSrc()
        {
            var se = @"
<[Script Dst=""..:Dest"" Src=""..:Source""]>
<[/Script]>
/*<[Raw Name=""Source""]>
Writer.Write(""Hello World"");
<[/Raw]>*/
<[Raw Name=""Dest""]>
Will be overwritten
<[/Raw]>
            ".Trim().ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual(@"
<[Script Dst=""..:Dest"" Src=""..:Source""]>
<[/Script]>
/*<[Raw Name=""Source""]>
Writer.Write(""Hello World"");
<[/Raw]>*/
<[Raw Name=""Dest""]>
Hello World
<[/Raw]>".Trim().FixNL(), result);
        }
    }
}