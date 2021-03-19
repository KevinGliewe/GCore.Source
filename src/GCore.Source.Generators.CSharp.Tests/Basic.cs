using GCore.Source.Generators.CSharp.Elements;
using GCore.Source.Generators.CSharp.Properties.Variables;
using GCore.Source.Generators.CSharp.Properties.Variables.Components;
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
            var tree = new CSharpTree();

            var @class = tree.CreateNode<CSharpClass>("TestClass");
            tree.Root.AddChild(@class);

            @class.Modifier = CSharpModifier.Public;
            @class["varString"] = new CSharpBuiltinVariable("varString", "Hallo Welt");

            var @func = tree.CreateNode<CSharpFunction>("TestFunc");
            @class.AddChild(@func);

            @func.Modifier = CSharpModifier.Abstract | CSharpModifier.Public;

            var writer = new CodeWriter();
            @class.Render(writer);
            var code = writer.GenerateCode();
        }
    }
}