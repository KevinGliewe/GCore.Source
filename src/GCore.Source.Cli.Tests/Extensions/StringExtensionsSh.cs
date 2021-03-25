using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GCore.Source.Cli.Tests.Extensions
{
    public static class StringExtensionsSh
    {
        public static void Sh3(
            this string cmd,
            out Process process,
            string workingDirectory = ".",
            bool redirectStandardError = true) {
            string str1 = cmd.Replace("\"", "\\\"");
            string str2 = "/bin/bash";
            string str3 = "-c \"" + str1 + "\"";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                str2 = "cmd.exe";
                str3 = "/C \"" + str1 + "\"";
            }
            process = new Process() {
                StartInfo = new ProcessStartInfo() {
                    FileName = str2,
                    Arguments = str3,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = redirectStandardError,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = workingDirectory
                }
            };
            process.Start();
        }
    }
}