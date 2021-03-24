using GCore.Source.Extensions;
using GCore.Source.Generators.CSharp.Elements;
using GCore.Source.Generators.CSharp.Extensions;
using GCore.Source.Generators.CSharp.Properties;
using GCore.Source.Generators.Extensions;
using NUnit.Framework;

namespace GCore.Source.Generators.CSharp.Tests
{
    public class Basic
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var doc = new CSharpDocument(null, "TestDoc");

            var @class = doc.Add(new CSharpClass(doc, "TestClass", new CSharpNamespace("TestNamespace")));

            @class.AddVarBoolean("m_boolVar");

            var @func = @class.Add(new CSharpFunction(@class, "TestFunc", CSharpType.Void, CSharpModifier.Abstract));

            var result = doc.Render().Trim().SplitNewLine();

            Assert.AreEqual(result[0] , "using System;");
            Assert.AreEqual(result[1] , "using System.Linq;");
            Assert.AreEqual(result[2] , "namespace TestNamespace {");
            Assert.AreEqual(result[3] , "    class TestClass{");
            Assert.AreEqual(result[4] , "        System.Boolean m_boolVar;");
            Assert.AreEqual(result[5] , "");
            Assert.AreEqual(result[6] , "        abstract System.Void TestFunc();");
            Assert.AreEqual(result[7] , "");
            Assert.AreEqual(result[8] , "");
            Assert.AreEqual(result[9] , "    }");
            Assert.AreEqual(result[10], "} // namespace TestNamespace");


        }
    }
}