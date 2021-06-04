using GCore.Source.Generators.Extensions;
using NUnit.Framework;

namespace GCore.Source.Generators.Scripting.Tests
{
    public class T42ElementTests
    {
        [SetUp]
        public void Setup() {
            // Need to reference this Type, so the Assembly loads into memory
            typeof(T42Element).GetType();
        }

        [Test]
        public void Simple() {
            var se = "<[T42 Dst=\".\" RenderTags=\"false\"]>\n<#=\"Hello\"#>\n<[/T42]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual("Hello", result);
        }

        [Test]
        public void Nuget() {
            var se =
@"
<[T42 Dst=""."" RenderTags=""false""]>
<#
    #r ""nuget: Frank.Libraries.Constants, 3.0.0""
    using Frank.Libraries.Constants.Gaming.Cards;
#>
<#=CardCharacters.Others.Back#>
<[/T42]>".ParseToSourceElement();

            var result = se.Render().Trim();

            Assert.AreEqual("🂠", result);
        }
    }
}