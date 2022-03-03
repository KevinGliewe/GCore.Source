using System;
using System.Collections.Generic;
using System.Reflection;
using GCore.Extensions.StringEx.Inject;
using GCore.Source.Extensions;

namespace GCore.Source.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigAttribute : Attribute
    {
        public string Name { get; }

        public ConfigAttribute(string name)
        {
            Name = name;
        }

        public static void ApplyAttribute(object dst, IReadOnlyDictionary<string, string> props,
            IReadOnlyDictionary<string, string>? inject)
        {
            foreach (var prop in dst.GetType().GetProperties())
            {
                var attr = prop.GetCustomAttribute(typeof(ConfigAttribute)) as ConfigAttribute;

                if(attr is null)
                    continue;

                if(!props.ContainsKey(attr.Name))
                    continue;

                var value = props[attr.Name];

                // inject values
                //if(!(inject is null))
                //    foreach (var pair in inject)
                //        value = value.Replace($"$({pair.Key})", pair.Value);

                value = value.SimpleInject(inject);

                var convertedValue = Convert.ChangeType(value, prop.PropertyType);

                prop.SetValue(dst, convertedValue);
            }
        }
    }
}