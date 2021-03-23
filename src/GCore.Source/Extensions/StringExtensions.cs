using System;

namespace GCore.Source.Extensions
{
    public static class StringExtensions
    {
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
    }
}