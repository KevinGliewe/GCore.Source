using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GCore.Source.Extensions
{
    public static class StringExtensions
    {
        // https://regex101.com/r/UB8UmT/1
        public static readonly string PARAMETERS_PATTERN = @"(?<name>\S+)=\""(?<value>([^\""]|\\"")*[^\\])?\""";
        public static readonly Regex PARAMETERS_REGEX = new Regex(PARAMETERS_PATTERN);

        /// <summary>
        /// Splits the string into lines based on '\r\n', '\r' or '\n'
        /// </summary>
        /// <param name="this">String with lines</param>
        /// <returns>Individual lines</returns>
        public static string[] SplitNewLine(this string @this)
        {
            return @this.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );
        }

        /// <summary>
        /// Extracts Parameters from string.
        /// Expects a format like 'x="y" z="w"'
        /// </summary>
        /// <param name="this">String containing Parameters</param>
        /// <returns>Parameters</returns>
        public static IReadOnlyDictionary<string, string> ExtractParameters(this string @this)
        {
            var match = PARAMETERS_REGEX.Matches(@this);

            var ret = new Dictionary<string, string>();

            foreach(Match m in match)
                ret.Add(m.Groups["name"].Value, m.Groups["value"].Value);

            return ret;
        }
    }
}