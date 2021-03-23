using System;
using GCore.Source.Generators.CSharp.Elements;
using GCore.Source.Generators.CSharp.Extensions;
using GCore.Source.Generators.CSharp.Properties;
using NUnit.Framework;

namespace GCore.Source.Generators.CSharp.Tests
{
    public class CSharpVariable_Basic
    {
        public static string VarName = "VarName";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Minimal()
        {
            Assert.AreEqual(
                $"var {VarName};{Environment.NewLine}", 
                new CSharpVariable(null, VarName).Render());
        }

        [Test]
        public void Minimal_Int32() {
            Assert.AreEqual(
                $"System.Int32 {VarName};{Environment.NewLine}", 
                new CSharpVariable(null, VarName, CSharpType.Int32).Render());
        }

        [Test]
        public void Minimal_Int32_42() {
            Assert.AreEqual(
                $"System.Int32 {VarName} = 42;{Environment.NewLine}", 
                new CSharpVariable(null, VarName, (Int32)42).Render());
        }

        [Test]
        public void Minimal_public_Int32_42() {
            Assert.AreEqual(
                $"public System.Int32 {VarName} = 42;{Environment.NewLine}", 
                new CSharpVariable(null, VarName, (Int32)42, CSharpModifier.Public).Render());
        }
    }
}