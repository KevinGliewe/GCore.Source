using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using GCore.Source.Scripting.Nuget;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;

namespace GCore.Source.Scripting
{
    public class ScriptRunner
    {
        private readonly InteractiveAssemblyLoader assemblyLoader;
        private readonly NugetMetadataResolver nugetResolver;
        private ScriptOptions scriptOptions;
        private ScriptState<object>? state;

        public ScriptRunner(Assembly[] references, string[] imports) {
            this.assemblyLoader = new InteractiveAssemblyLoader(new MetadataShadowCopyProvider());
            this.nugetResolver = new NugetMetadataResolver(new ReferenceAssemblyService().ReferenceAssemblyPaths);
            this.scriptOptions = ScriptOptions.Default
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