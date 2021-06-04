using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GCore.Source.Scripting.Tests
{
    public class ScriptRunnerTests
    {
        [Test]
        public async Task ReturnValue()
        {
            var runner = new ScriptRunner(new Assembly[0], new string[0]);

            var result = await runner.Run("\"Hello World\"", null);

            Assert.IsTrue(result is EvaluationResult.Success);

            var success = result as EvaluationResult.Success;

            Assert.AreEqual("Hello World", success?.ReturnValue);
        }

        public class Globals
        {
            public string Test = "Hello ";
        }

        [Test]
        public async Task GlobalValue()
        {

            var global = new Globals();

            var runner = new ScriptRunner(new Assembly[0], new string[0]);

            var result = await runner.Run("Test += \"World\";", global);

            Assert.IsTrue(result is EvaluationResult.Success);

            var success = result as EvaluationResult.Success;

            Assert.AreEqual("Hello World", global.Test);
        }

        [Test]
        public async Task NugetReturnValue() {
            var runner = new ScriptRunner(new Assembly[0], new string[0]);

            var result = await runner.Run(
            @"
                #r ""nuget: Frank.Libraries.Constants, 3.0.0""
                using Frank.Libraries.Constants.Gaming.Cards;
                CardCharacters.Others.Back
            ", null);

            Assert.IsTrue(result is EvaluationResult.Success);

            var success = result as EvaluationResult.Success;

            Assert.AreEqual("🂠", success?.ReturnValue);
        }
    }
}