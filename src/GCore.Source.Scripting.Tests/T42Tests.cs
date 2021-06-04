using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GCore.Source.Scripting.Tests
{
    public class T42Tests
    {
        public class Globals
        {
            public TextWriter Writer = new StringWriter();
        }

        [Test]
        public async Task NoCodeRun() {

            var code = T42Template.T42ToScript("Hello World");

            var global = new Globals();

            var runner = new ScriptRunner(new Assembly[0], new string[0]);

            var result = await runner.Run(code, global);

            Assert.IsTrue(result is EvaluationResult.Success);

            var success = result as EvaluationResult.Success;

            var resultTemplate = global.Writer.ToString();

            Assert.AreEqual("Hello World", resultTemplate);
        }

        [Test]
        public async Task BasicRun()
        {

            var code = T42Template.T42ToScript("<#for(int i = 0; i < 10; i++){#>\n<#=i#><#}#>");

            var global = new Globals();

            var runner = new ScriptRunner(new Assembly[0], new string[0]);

            var result = await runner.Run(code, global);

            Assert.IsTrue(result is EvaluationResult.Success);

            var success = result as EvaluationResult.Success;

            var resultTemplate = global.Writer.ToString();

            Assert.AreEqual("\n0\n1\n2\n3\n4\n5\n6\n7\n8\n9", resultTemplate);
        }

        [Test]
        public async Task NugetRun() {

            var code = T42Template.T42ToScript(
            @"
            <#
                #r ""nuget: Frank.Libraries.Constants, 3.0.0""
                using Frank.Libraries.Constants.Gaming.Cards;
            #>
            <#=CardCharacters.Others.Back#>
            ");

            var global = new Globals();

            var runner = new ScriptRunner(new Assembly[0], new string[0]);

            var result = await runner.Run(code, global);

            Assert.IsTrue(result is EvaluationResult.Success);

            var success = result as EvaluationResult.Success;

            var resultTemplate = global.Writer.ToString()?.Trim();

            Assert.AreEqual("🂠", resultTemplate);
        }
    }
}