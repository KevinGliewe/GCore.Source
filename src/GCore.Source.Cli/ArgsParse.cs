using System;
using System.Collections.Generic;

namespace GCore.Source.Cli
{
    public static class ArgsParse
    {
        public static IDictionary<string, string?> Parse(string[] args)
        {
            var ret = new Dictionary<string, string?>();

            string? currentKey = null;

            foreach (var s in args)
            {
                if (s.StartsWith("--"))
                {
                    if(!(currentKey is null))
                        ret[currentKey] = null;

                    currentKey = s[2..];
                }
                else
                {
                    ret[currentKey ?? throw new Exception($"Args: Value '{s}' without key!")] = s;
                    currentKey = null;
                }
            }

            if (!(currentKey is null))
                ret[currentKey] = null;

            return ret;
        }
    }
}