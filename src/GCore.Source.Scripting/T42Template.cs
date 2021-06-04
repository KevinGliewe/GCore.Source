using System;
using System.CodeDom.Compiler;
using System.IO;
using GCore.Extensions.StringEx;

namespace GCore.Source.Scripting
{
    public class T42Template
    {
        public static void T42ToScript(TextWriter tw, string t42, string delimiterStart = "<#",
            string delimiterEnd = "#>", string writeFunction = "Writer.Write")
        {
            var splitDelStart = t42.Split(delimiterStart);

            bool isFirst = true;

            foreach (var splitStart in splitDelStart)
            {
                var splitDelEnd = splitStart.Split(delimiterEnd);

                if (isFirst)
                {
                    if (splitDelEnd.Length != 1)
                        throw new Exception("Delimiter mismatch!");

                    if(splitDelEnd[0].Trim().Length > 0)
                        WriteConstant(tw, splitDelEnd[0], writeFunction);
                }
                else
                {
                    if (splitDelEnd.Length != 2)
                        throw new Exception("Delimiter mismatch!");
                    WriteCode(tw, splitDelEnd[0], writeFunction);
                    WriteConstant(tw, splitDelEnd[1], writeFunction);
                }


                isFirst = false;
            }
        }

        public static string T42ToScript(string t42, string delimiterStart = "<#", string delimiterEnd = "#>",
            string writeFunction = "Writer.Write")
        {
            var tw = new StringWriter();
            T42ToScript(tw, t42, delimiterStart, delimiterEnd, writeFunction);
            return tw.ToString();
        }

        private static void WriteCode(TextWriter tw, string code, string writeFunction)
        {
            if (code.StartsWith("="))
            {
                code = code.Substring(1, code.Length - 1);

                tw.WriteLine($"{writeFunction}($\"{{{code}}}\");");
            }
            else
            {
                tw.WriteLine(code);
            }
        }

        private static void WriteConstant(TextWriter tw, string code, string writeFunction)
        {
            if(code.Length > 0)
                tw.WriteLine($"{writeFunction}({code.Escape()});");
        }
    }
}