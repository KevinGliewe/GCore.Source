using System.Collections.Generic;

namespace GCore.Source
{
    public interface IConfigurable
    {
        void Configure(IReadOnlyDictionary<string, string> config);
    }
}