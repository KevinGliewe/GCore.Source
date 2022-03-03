using System;
using System.Linq;

namespace GCore.Source.Helper
{
    public static class StringHelper
    {
        public static string[] Split(this string self, string deli)
        {
            return self.Split(new string[] { deli }, StringSplitOptions.None);
        }

        public static string GetSpaces(int n)
        {
            return string.Concat(Enumerable.Repeat(" ", n));
        }

        public static string ReplaceChar(string text, int pos, String replace)
        {
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + 1);
        }

        public const char DEFAULTALIGN = '§';
        public const int DEFAULTINDENT = 4;

        public static void Align(ref string[] lines, char align = DEFAULTALIGN, int indent = DEFAULTINDENT)
        {
            int maxIndent = -1;

            for (int e = 0; e < 20; e++)
            {
                maxIndent = -1;
                foreach (var line in lines)
                {
                    var i = line.IndexOf(align);
                    if (i > maxIndent)
                        maxIndent = i;
                }

                if (maxIndent < 0)
                    break;

                int currindent = ((maxIndent / indent + 1)) * indent;

                for (int l = 0; l < lines.Length; l++)
                {
                    var i = lines[l].IndexOf(align);
                    var spaces = GetSpaces(currindent - i);
                    lines[l] = ReplaceChar(lines[l], i, spaces);
                }
            }

            for (int l = 0; l < lines.Length; l++)
                lines[l] = lines[l].TrimEnd();
        }

        public static string[] Align(string[] lines, char align = DEFAULTALIGN, int indent = DEFAULTINDENT)
        {
            var ret = (string[]) lines.Clone();
            Align(ref ret, align, indent);
            return ret;
        }

        public static string Align(string code, char align = DEFAULTALIGN, int indent = DEFAULTINDENT)
        {
            var lines = code.Split(Environment.NewLine);
            Align(ref lines, align, indent);
            return String.Join(Environment.NewLine, lines);
        }
    }
}