using System;
using System.IO;
using System.Reflection;

namespace GCore.Source.Helper
{
    public static class TypeHelper
    {
        public static Type? RequestType(string typeName)
        {
            System.Type? type = null;

            if (typeName.Contains("?")) {
                var splitName = typeName.Split('?');
                var assembly = LoadAssembly(splitName[0]);
                type = assembly.FindType(splitName[1]);
            } else {
                type = System.Type.GetType(typeName);

                if (type is null)
                {
                    var fullName = typeName.Contains(".");

                    // All loaded assemblies
                    foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        type = ass.FindType(typeName);

                        if (!(type is null))
                            return type;
                    }
                }
            }

            return type;
        }

        public static Type? FindType(this Assembly @this, string typeName)
        {
            var fullName = typeName.Contains(".");
            foreach (var t in @this.GetTypes()) {
                if (typeName == (fullName ? t.FullName : t.Name))
                    return t;
            }
            return null;
        }

        public static Assembly LoadAssembly(string assemblyFilename)
        {
            var fullName = Path.GetFullPath(assemblyFilename);
            Assembly assembly = Assembly.LoadFile(fullName);
            return assembly;
        }

        public static int CombineHash(params object?[] obj)
        {
            // https://stackoverflow.com/a/1646913
            int hash = 17;

            foreach(var o in obj)
                if(!(o is null))
                    hash = hash * 31 + o.GetHashCode();
                    
            return hash;
        }
    }
}