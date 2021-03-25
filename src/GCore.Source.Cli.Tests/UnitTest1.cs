using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

using GCore.Extensions.StringShEx;
using GCore.Source.Cli.Tests.Extensions;

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
<[/Raw]>".Trim();

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
            Assert.AreEqual(Expected + Environment.NewLine, process.StandardOutput.ReadToEnd());
        }

        [Test]
        public void FileStdOut()
        {
            Process process;

            $@"dotnet GCore.Source.Cli.dll --in {TestFile}".Sh3(out process);

            process.WaitForExit();

            Assert.AreEqual("", process.StandardError.ReadToEnd());
            Assert.AreEqual(0, process.ExitCode);
            Assert.AreEqual(Expected, process.StandardOutput.ReadToEnd());
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
            Assert.AreEqual(Expected, File.ReadAllText(tmpFile));
        }
    }
}