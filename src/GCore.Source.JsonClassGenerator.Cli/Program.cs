using System;
using System.IO;
using System.Text;
using GCore.Source.JsonClassGenerator.CodeWriters;

namespace GCore.Source.JsonClassGenerator.Cli
{
    public enum Lang
    {
        CSharp,
        Java,
        Php,
        Sql,
        TypeScript,
        VisualBasic
    }

    class Program
    {
        static void Main(
            string json = "[stdin]", 
            string write = "[stdout]",
            Lang lang = Lang.CSharp,
            string MainClass = "Root",
            string Namespace = null,
            string SecondaryNamespace = null,
            bool InternalVisibility = false,
            bool ExplicitDeserialization = false,
            bool UseNestedClasses = false,
            bool ApplyObfuscationAttributes = false,
            bool ExamplesInDocumentation = false,
            bool UsePascalCase = false,
            bool UseProperties = false,
            string PropertyAttribute = null
            ) {


            try
            {
                var code = ReadInput(json);
                var writer = GetWriter(lang);


                var gen = new JsonClassGenerator();
                gen.Example = code;
                gen.InternalVisibility = InternalVisibility;
                gen.CodeWriter = writer;
                gen.ExplicitDeserialization = ExplicitDeserialization;
                gen.Namespace = Namespace;
                gen.NoHelperClass = true;
                gen.SecondaryNamespace = SecondaryNamespace;
                gen.UseProperties = UseProperties;
                gen.MainClass = MainClass;
                gen.UsePascalCase = UsePascalCase;
                gen.PropertyAttribute = PropertyAttribute;

                gen.UseNestedClasses = UseNestedClasses;
                gen.ApplyObfuscationAttributes = ApplyObfuscationAttributes;
                gen.SingleFile = true;
                gen.ExamplesInDocumentation = ExamplesInDocumentation;

                gen.TargetFolder = null;
                gen.SingleFile = true;

                using (var sw = new StringWriter()) {
                    gen.OutputStream = sw;
                    gen.GenerateClasses();
                    sw.Flush();

                    WriteOutput(sw.ToString(), write);
                }


                Environment.Exit(0);
            } 
            catch (Exception ex) {
                Console.Error.WriteLine(ex.ToString());
                Environment.Exit(ex.HResult);
            }
        }

        static string ReadInput(string input) {

            if (input == "[stdin]") {
                return ReadStdin();
            } else {
                return ReadFile(input);
            }
        }

        static void WriteOutput(string data, string output) {
            if (output == "[stdout]") {
                WriteStdout(data);
            } else {
                WriteFile(output, data);
            }
        }

        static string ReadStdin() {
            var sb = new StringBuilder();
            string input = null;

            while (!string.IsNullOrEmpty(input = Console.ReadLine())) {
                sb.AppendLine(input);
            }

            return sb.ToString();
        }

        static string ReadFile(string file) {
            if (!File.Exists(file))
                throw new Exception("Input: File does not exists: " + file);

            return File.ReadAllText(file);
        }

        static void WriteStdout(string data) {
            Console.Write(data);
        }

        static void WriteFile(string file, string data) {
            if (!File.Exists(file))
                throw new Exception("Output: File does not exists: " + file);

            File.WriteAllText(file, data);
        }

        static ICodeWriter GetWriter(Lang lang)
        {
            switch (lang)
            {
                case Lang.CSharp:
                    return new CSharpCodeWriter();
                case Lang.Java:
                    return new JavaCodeWriter();
                case Lang.Php:
                    return new PhpCodeWriter();
                case Lang.Sql:
                    return new SqlCodeWriter();
                case Lang.TypeScript:
                    return new TypeScriptCodeWriter();
                case Lang.VisualBasic:
                    return new VisualBasicCodeWriter();
            }
            throw new Exception("Lang not found");
        }
    }
}
