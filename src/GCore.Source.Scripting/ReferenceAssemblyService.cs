// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using GCore.Source.Scripting.Helper;

namespace GCore.Source.Scripting
{
    /// <remarks>
    /// Useful notes https://github.com/dotnet/roslyn/blob/main/docs/wiki/Runtime-code-generation-using-Roslyn-compilations-in-.NET-Core-App.md
    /// </remarks>
    public class ReferenceAssemblyService
    {

        public IReadOnlyCollection<string> ReferenceAssemblyPaths { get; }

        public ReferenceAssemblyService(string framework_ = "Microsoft.NETCore.App")
        {

            var (framework, version) = GetDesiredFrameworkVersion(framework_);
            var sharedFrameworks = GetSharedFrameworkConfiguration(framework, version);

            this.ReferenceAssemblyPaths = sharedFrameworks.Select(framework => framework.ReferencePath).ToArray();
        }

        private SharedFramework[] GetSharedFrameworkConfiguration(string framework, string version)
        {
            var referencePath = GetCurrentAssemblyReferencePath(framework, version);
            var implementationPath = GetCurrentAssemblyImplementationPath(framework, version);

            var referenceDlls = Directory.GetFiles(referencePath, "*.dll", SearchOption.TopDirectoryOnly);
            var implementationDlls = Directory.GetFiles(implementationPath, "*.dll", SearchOption.TopDirectoryOnly);

            // Microsoft.NETCore.App is always included.
            // If we're including e.g. Microsoft.AspNetCore.App, include it alongside Microsoft.NETCore.App.
            return framework switch
            {
                SharedFramework.NetCoreApp => new[]
                {
                    new SharedFramework(referencePath, implementationPath, referenceDlls, implementationDlls)
                },
                _ => GetSharedFrameworkConfiguration(SharedFramework.NetCoreApp, version)
                    .Append(new SharedFramework(referencePath, implementationPath, referenceDlls, implementationDlls))
                    .ToArray()
            };
        }

        private static (string framework, string version) GetDesiredFrameworkVersion(string sharedFramework)
        {
            var parts = sharedFramework.Split('/');
            if (parts.Length == 2)
            {
                return (parts[0], parts[1]);
            }
            else if (parts.Length == 1)
            {
                return (parts[0], Environment.Version.Major.ToString());
            }
            else
            {
                throw new InvalidOperationException("Unknown Shared Framework configuration: " + sharedFramework);
            }
        }

        private static string GetCurrentAssemblyReferencePath(string framework, string version) {
            var dotnetRuntimePath = RuntimeEnvironment.GetRuntimeDirectory();
            var dotnetRoot = Path.GetFullPath(Path.Combine(dotnetRuntimePath, "../../../packs/", framework + ".Ref"));
            var referenceAssemblyPath = Directory
                .GetDirectories(dotnetRoot, "net*" + version + "*", SearchOption.AllDirectories)
                .Last();
            return Path.GetFullPath(referenceAssemblyPath);
        }

        private static string GetCurrentAssemblyImplementationPath(string framework, string version) {
            var currentFrameworkPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location ?? throw new Exception()) ?? throw new Exception(), ".."));
            var configuredFramework = currentFrameworkPath.Replace(SharedFramework.NetCoreApp, framework);
            var configuredFrameworkAndVersion = Directory
                .GetDirectories(configuredFramework, version + "*")
                .OrderBy(path => new Version(Path.GetFileName(path)))
                .Last();

            return configuredFrameworkAndVersion;
        }

        public record SharedFramework(string ReferencePath, string ImplementationPath, string[] ReferenceAssemblies, string[] ImplementationAssemblies)
        {
            public const string NetCoreApp = "Microsoft.NETCore.App";

            public static IReadOnlyCollection<string> SupportedFrameworks { get; } =
                Directory
                    .GetDirectories(
                        Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location) ?? throw new Exception(), "../../")
                    )
                    .Select(Path.GetFileName)
                    .Select(p => p ?? throw new Exception())
                    .ToArray();
        }
    }
}
