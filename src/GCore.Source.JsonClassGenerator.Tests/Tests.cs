using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using GCore.Source.JsonClassGenerator;
using GCore.Source.JsonClassGenerator.CodeWriters;

namespace GCore.Source.JsonClassGenerator.Tests
{
    public class Tests
    {

        public static readonly string JsonData = @"
{
  ""employees"": [
    {
      ""firstName"": ""John"",
      ""lastName"": ""Doe""
    },
    {
      ""firstName"": ""Anna"",
      ""lastName"": ""Smith""
    },
    {
      ""firstName"": ""Peter"",
      ""lastName"": ""Jones ""
    }
  ]
}".Trim();

        public static readonly string CSharpExpected = @"
namespace TestNamespace
{

    public class Employee
    {

        public string firstName;

        public string lastName;
    }

    public class MainClass
    {

        public IList<Employee> employees;
    }

}
".Trim();

        public static string Generate(ICodeWriter langWriter)
        {
            var gen = new JsonClassGenerator();
            gen.Example = JsonData;
            gen.InternalVisibility = false;
            gen.CodeWriter = langWriter;
            gen.ExplicitDeserialization = false;
            gen.Namespace = "TestNamespace";
            gen.NoHelperClass = false;
            gen.SecondaryNamespace = null;
            //gen.UseProperties = (language != 5 && language != 6) || hasGetSet;
            gen.MainClass = "MainClass";
            //gen.UsePascalCase = pascal;
            //gen.PropertyAttribute = propertyAttribute;

            gen.UseNestedClasses = false;
            gen.ApplyObfuscationAttributes = false;
            gen.SingleFile = true;
            gen.ExamplesInDocumentation = false;

            gen.TargetFolder = null;
            gen.SingleFile = true;

            using (var sw = new StringWriter()) {
                gen.OutputStream = sw;
                gen.GenerateClasses();
                sw.Flush();

                return sw.ToString().Trim();
            }
        }

        [SetUp]
        public void Setup() {
        }

        [Test]
        public void CSharp()
        {
            var code = Generate(new CSharpCodeWriter());

            Assert.AreEqual(CSharpExpected, code);
        }

        [Test]
        public void CSharp2()
        {
            var code = JsonClassGenerator.Generate(JsonData, MainClass: "MainClass", Namespace: "TestNamespace").Trim();

            Assert.AreEqual(CSharpExpected, code);
        }

        /*[Test]
        public void StdInStdOut() {
            Process process;

            @"dotnet JsonClassGenerator.Cli.dll -- --namespace TestNamespace --main-class MainClass".Sh3(out process);

            process.StandardInput.Write(JsonData);
            process.StandardInput.Close();

            process.WaitForExit();

            Assert.AreEqual("", process.StandardError.ReadToEnd());
            Assert.AreEqual(0, process.ExitCode);
            Assert.AreEqual(CSharpExpected + Environment.NewLine, process.StandardOutput.ReadToEnd());
        }*/
    }
}