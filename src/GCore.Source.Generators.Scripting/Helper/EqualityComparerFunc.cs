using System;
using System.Collections.Generic;

namespace GCore.Source.Generators.Scripting.Helper
{
    public class EqualityComparerFunc<T> : IEqualityComparer<T>
    {
        readonly Func<T, T, bool> _comparer;
        readonly Func<T, int> _hash;

        public EqualityComparerFunc(Func<T, T, bool> comparer, Func<T, int> hash) {
            _comparer = comparer;
            _hash = hash;
        }

        public bool Equals(T? x, T? y)
        {
            if (x is null)
                return y is null;
            if (y is null)
                return x is null;

            return _comparer(x, y);
        }
        public int GetHashCode(T obj) => _hash(obj);
    }
}