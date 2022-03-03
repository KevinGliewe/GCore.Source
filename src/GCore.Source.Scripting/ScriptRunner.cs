using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using GCore.Source.Scripting.Nuget;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using NuGet.PackageManagement;

namespace GCore.Source.Scripting
{
    public class ScriptRunner
    {
        public const string CONFIG_SEARCHPATHS = "searchpaths";
        public const string CONFIG_BASEDIRECTORY = "basedirectory";

        private readonly InteractiveAssemblyLoader assemblyLoader;
        private readonly NugetMetadataResolver nugetResolver;
        private ScriptOptions scriptOptions;
        private ScriptState<object>? state;

        public ScriptRunner(Assembly[] references, string[] imports, string[]? searchPaths = null, string? baseDirectory = null, IReadOnlyDictionary<string, string>? config = null)
        {
            var _searchPaths = searchPaths is null ? new List<string>() : new List<string>(searchPaths);

            if (config is not null)
            {
                if(config.ContainsKey(CONFIG_SEARCHPATHS))
                    _searchPaths.AddRange(config[CONFIG_SEARCHPATHS].Split(';'));

                if (baseDirectory is null && config.ContainsKey(CONFIG_BASEDIRECTORY))
                    baseDirectory = config[CONFIG_BASEDIRECTORY];
            }

            if (baseDirectory is null)
                baseDirectory = Directory.GetCurrentDirectory();


            this.assemblyLoader = new InteractiveAssemblyLoader(new MetadataShadowCopyProvider());
            this.nugetResolver = new NugetMetadataResolver(new ReferenceAssemblyService().ReferenceAssemblyPaths);
            this.scriptOptions = ScriptOptions.Default
                .WithSourceResolver(new RemoteFileResolver(_searchPaths.ToImmutableArray(), baseDirectory, config))
                .WithMetadataResolver(nugetResolver)
                .WithReferences(references)
                .WithAllowUnsafe(true)
                .AddImports(imports);
        }

        public async Task<EvaluationResult> Run(string text, object? globals) {
            try {
                var nugetCommands = text
                    .Split(new[] { '\r', '\n' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Where(nugetResolver.IsNugetReference);
                foreach (var nugetCommand in nugetCommands) {
                    var assemblyReferences = await nugetResolver.InstallNugetPackageAsync(nugetCommand, CancellationToken.None).ConfigureAwait(false);
                    this.scriptOptions = this.scriptOptions.AddReferences(assemblyReferences);
                }

                state = CSharpScript.RunAsync(text, scriptOptions, globals).Result;
                var evaluatedReferences = state.Script.GetCompilation().References.ToList();

                return state.Exception is null
                    ? new EvaluationResult.Success(text, state.ReturnValue, evaluatedReferences)
                    : new EvaluationResult.Error(state.Exception);
            } catch (Exception oce) when (oce is OperationCanceledException || oce.InnerException is OperationCanceledException) {
                // user can cancel by pressing ctrl+c, which triggers the CancellationToken
                return new EvaluationResult.Cancelled();
            } catch (Exception exception) {
                return new EvaluationResult.Error(exception);
            }
        }
    }

    public abstract record EvaluationResult
    {
        public record Success(string Input, object ReturnValue, IReadOnlyCollection<MetadataReference> References) : EvaluationResult;
        public record Error(Exception Exception) : EvaluationResult;
        public record Cancelled() : EvaluationResult;
    }
}