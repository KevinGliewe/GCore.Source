using System;
using System.Diagnostics;

namespace GCore.Source
{
    public enum BracketType
    {
        Round,
        Curly,
        Square,
        Angle,
    }

    public static class BracketTypeExtensions
    {

        private static char[] _brackets = new[]
        {
            '(', ')',
            '{', '}',
            '[', ']',
            '<', '>',
        };

        static BracketTypeExtensions()
        {
            Debug.Assert(_brackets.Length == Enum.GetValues(typeof(BracketType)).Length * 2);
        }

        public static char GetChar(this BracketType bt, bool open)
        {
            return _brackets[(int)bt * 2 + (open ? 0 : 1)];
        }
    }
}