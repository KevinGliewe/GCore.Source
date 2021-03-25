using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using GCore.Source.Extensions;

namespace GCore.Source.Cli.Tests.Extensions
{
    public static class StringExtensions
    {
        public static string FixNL(this string @this)
        {
            return string.Join(Environment.NewLine, @this.SplitNewLine());
        }
    }
}