using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

using GCore.Extensions.StringShEx;
using GCore.Source.Cli.Tests.Extensions;
using GCore.Source.Extensions;

namespace GCore.Source.Cli.Tests
{
    public class Tests
    {
        private static string Expected = @"
<[Script Dst=""..:out""]>
Writer.Write(""- Overwrite -"");
<[/Script]>
<[Raw Name=""out""]>
- Overwrite -
<[/Raw]>".Trim().FixNL();

        private static string TestFile = "TestData/InjectFile.txt";

        [SetUp]
        public void Setup() {
        }

        [Test]
        public void StdInStdOut()
        {
            Process process;

            @"dotnet GCore.Source.Cli.dll".Sh3(out process);

            process.StandardInput.Write(File.ReadAllText(TestFile));
            process.StandardInput.Close();

            process.WaitForExit();

            Assert.AreEqual("", process.StandardError.ReadToEnd());
            Assert.AreEqual(0, process.ExitCode);
            Assert.AreEqual(Expected + Environment.NewLine, process.StandardOutput.ReadToEnd().FixNL());
        }

        [Test]
        public void FileStdOut()
        {
            Process process;

            $@"dotnet GCore.Source.Cli.dll --in {TestFile}".Sh3(out process);

            process.WaitForExit();

            Assert.AreEqual("", process.StandardError.ReadToEnd());
            Assert.AreEqual(0, process.ExitCode);
            Assert.AreEqual(Expected, process.StandardOutput.ReadToEnd().FixNL());
        }

        [Test]
        public void FileConfig() {
            Process process;

            $@"dotnet GCore.Source.Cli.dll --RenderTagsArgs false --in TestData/InjectConfig.txt".Sh3(out process);

            process.WaitForExit();

            Assert.AreEqual("", process.StandardError.ReadToEnd());
            Assert.AreEqual(0, process.ExitCode);
            Assert.AreEqual("Hello World", process.StandardOutput.ReadToEnd().Trim());
        }

        [Test]
        public void FileIn() {
            Process process;

            var tmpFile = "./TestData/InjectFile_.txt";

            if(File.Exists(tmpFile))
                File.Delete(tmpFile);

            File.Copy(TestFile, tmpFile);

            $@"dotnet GCore.Source.Cli.dll --in {tmpFile} --out [in]".Sh3(out process);

            process.WaitForExit();

            Assert.AreEqual("", process.StandardError.ReadToEnd());
            Assert.AreEqual(0, process.ExitCode);
            Assert.AreEqual(Expected, File.ReadAllText(tmpFile).FixNL());
        }

        [Test]
        public void StdInStdOutAligned()
        {
            var infile = "./TestData/InjectFileAligned.txt";
            var expectfile = "./TestData/InjectFileAligned.expected.txt";

            Process process;

            @"dotnet GCore.Source.Cli.dll".Sh3(out process);

            process.StandardInput.Write(File.ReadAllText(infile));
            process.StandardInput.Close();

            process.WaitForExit();

            Assert.AreEqual("", process.StandardError.ReadToEnd());
            Assert.AreEqual(0, process.ExitCode);

            var linesExpected = File.ReadAllText(expectfile).FixNL().SplitNewLine();
            var lines = process.StandardOutput.ReadToEnd().FixNL().SplitNewLine();

            for (int i = 0; i < Math.Min(linesExpected.Length, lines.Length); i++)
                Assert.AreEqual(linesExpected[i], lines[i]);

            Assert.AreEqual(linesExpected.Length+1, lines.Length);
        }
    }
}