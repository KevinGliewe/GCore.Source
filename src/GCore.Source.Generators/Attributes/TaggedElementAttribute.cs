using System;
using System.Collections.Generic;

namespace GCore.Source.Generators.Attributes
{
    public interface ITaggedElement : IConfigurable
    {
        void SetStartLine(string start);
        void SetStopLine(string stop);
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TaggedElementAttribute : Attribute
    {
        public string Tag { get; }

        public TaggedElementAttribute(string tag)
        {
            Tag = tag;
        }

        public static IReadOnlyDictionary<string, Type> GetAll()
        {
            var ret = new Dictionary<string, Type>();

            // All loaded assemblies
            foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in ass.GetTypes())
                {
                    // Check it type is ITaggedElement
                    if(!typeof(ITaggedElement).IsAssignableFrom(type))
                        continue;
                    
                    var attr = type.GetCustomAttributes(typeof(TaggedElementAttribute), true);

                    if(attr.Length == 0)
                        continue;

                    var tag = (attr[0] as TaggedElementAttribute)?.Tag ?? throw new Exception("Where is the guru?");

                    ret.Add(tag, type);
                }
            }

            return ret;
        }

        public static Type? GetFromTag(string tag)
        {
            var types = GetAll();

            if (types.ContainsKey(tag))
                return types[tag];
            return null;
        }

        public static string? GetTag(Type type)
        {
            var attr = type.GetCustomAttributes(typeof(TaggedElementAttribute), true);

            if (attr.Length == 0)
                return null;

            return (attr[0] as TaggedElementAttribute)?.Tag ?? throw new Exception("Where is the guru?");
        }
    }
}