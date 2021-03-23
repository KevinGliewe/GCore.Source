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

            var code = doc.Render();
        }
    }
}