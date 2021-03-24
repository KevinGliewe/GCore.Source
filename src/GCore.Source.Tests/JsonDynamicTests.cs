﻿using System.IO;
using GCore.Source.Helper;
using NUnit.Framework;

namespace GCore.Source.Tests
{
    public class JsonDynamicTests
    {
        private JsonDynamic json = JsonDynamic.Parse(File.ReadAllText("TestData/TestModel.json"));

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Query()
        {
            Assert.AreEqual("file", json.Query("menu.id"));
            Assert.AreEqual(42, json.Query("menu.int_"));
            Assert.AreEqual(3.14159, json.Query("menu.float_"));
            Assert.AreEqual(true, json.Query("menu.bool_"));
            Assert.AreEqual("Open", json.Query("menu.popup.menuitem[1].value"));
        }

        [Test]
        public void Dynamic()
        {
            dynamic d = json;

            Assert.AreEqual("file", d.menu.id);
            Assert.AreEqual(42, d.menu.int_);
            Assert.AreEqual(3.14159, d.menu.float_);
            Assert.AreEqual(true, d.menu.bool_);
            Assert.AreEqual("Open", d.menu.popup.menuitem[1].value);
        }

        [Test]
        public void FileQuery()
        {
            Assert.AreEqual(42, JsonDynamic.QueryFile("TestData/TestModel.json?menu.int_"));
        }
    }
}