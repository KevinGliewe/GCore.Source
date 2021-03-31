using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GCore.Source.Extensions;
using GCore.Source.Generators.Extensions;
using GCore.Source.Generators.Razor;
using GCore.Source.Generators.Scripting;

namespace GCore.Source.Cli
{
    class Program
    {
        public static IDictionary<string, string?> Args { get; private set; } = new Dictionary<string, string?>();

        static IDictionary<string, string?> DefaultArgs = new Dictionary<string, string?>()
        {
            {"in", "[stdin]"},
            {"out", "[stdout]"},
            {"BaseDir", Environment.CurrentDirectory},
            {"InDir", Environment.CurrentDirectory},
            {"OutDir", Environment.CurrentDirectory},
        };

        static void InjectDefaultArgs(IDictionary<string, string?> args)
        {
            foreach (var def in DefaultArgs)
            {
                if (!args.ContainsKey(def.Key))
                    args.Add(def);
                else if (args[def.Key] is null)
                    args[def.Key] = def.Value;
            }
        }

        static bool Flag(string key)
        {
            return Args.ContainsKey(key) && Args[key] == null;
        }

        static IReadOnlyDictionary<string, string> GetConfig()
        {
            var ret = new Dictionary<string, string>();

            foreach (var kv in Args)
            {
                ret.Add(kv.Key, kv.Value ?? "");
            }

            return ret;
        }

        static void Main(string[] args) {
            try
            {
                typeof(ScriptElement).GetType();
                typeof(RazorElement).GetType();

                var args_ = ArgsParse.Parse(args);
                InjectDefaultArgs(args_);
                Args = args_;

                if (Flag("help"))
                {
                    Console.WriteLine(@"
gsource:
    --in:      Input source, [stdin] for StdIn or file path.
               Default: [stdin]
    
    --out:     Output sink, [stdout] for StdOut, [in] for same file as --in or file path.
               Default: [stdout]

    --BaseDir: BaseDir, used for config injection.
               Default: Current directory

    --InDir:   InDir, used for config injection.
               Default: Director of --in if file, else Current directory

    --OutDir:  OutDir, used for config injection.
               Default: Director of --out if file, else Current directory

EXAMPLE:

Reads and writes to the same file:
    gsource --in injectfile.txt --out [in]

                    ");
                    Environment.Exit(0);
                }

                if (Flag("version"))
                {
                    Console.WriteLine("GCore.Source.Cli: " + AssemblyVersionConstants.Version);
                    Environment.Exit(0);
                }

                var data = ReadInput();

                var se = data.ParseToSourceElement();

                se.Configure(GetConfig());

                var output = se.Render();

                WriteOutput(output);

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                Environment.Exit(ex.HResult);
            }
        }

        static string ReadInput()
        {
            var inName = Args["in"] ?? throw new Exception();

            if (inName == "[stdin]")
            {
                return ReadStdin();
            }
            else
            {
                return ReadFile(inName);
            }
        }

        static void WriteOutput(string data)
        {
            var outName = Args["out"] ?? throw new Exception();

            if (outName == "[stdout]")
            {
                WriteStdout(data);
            }
            else if (outName == "[in]")
            {
                var inName = Args["in"] ?? throw new Exception();

                if (inName == "[stdin]")
                    WriteOutput(data);
                else
                    WriteFile(inName, data);

            }
            else
            {
                WriteFile(outName, data);
            }
        }

        static string ReadStdin()
        {
            var sb = new StringBuilder();
            string? input = null;

            while (!string.IsNullOrEmpty(input = Console.ReadLine())) {
                sb.AppendLine(input);
            }

            return sb.ToString();
        }

        static string ReadFile(string file)
        {
            if(!File.Exists(file))
                throw new Exception("Input: File does not exists: " + file);

            Args["InDir"] = Path.GetDirectoryName(file);

            return File.ReadAllText(file);
        }

        static void WriteStdout(string data)
        {
            Console.Write(data);
        }

        static void WriteFile(string file, string data)
        {
            if (!File.Exists(file))
                throw new Exception("Output: File does not exists: " + file);

            Args["OutDir"] = Path.GetDirectoryName(file);

            File.WriteAllText(file, data);
        }
    }
}
