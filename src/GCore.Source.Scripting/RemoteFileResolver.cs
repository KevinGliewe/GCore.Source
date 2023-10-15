using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using System.Text;
using GCore.Extensions.StringEx.Inject;
using Microsoft.CodeAnalysis;

namespace GCore.Source.Scripting
{
    // https://github.com/filipw/Roslyn.Resolvers.Demo
    public class RemoteFileResolver : SourceReferenceResolver
    {
        private readonly Dictionary<string, string> _remoteFiles = new Dictionary<string, string>();
        private readonly SourceFileResolver _fileBasedResolver;
        private readonly IReadOnlyDictionary<string, string>? _config;

        public RemoteFileResolver(IReadOnlyDictionary<string, string>? config = null) : this(ImmutableArray.Create(new string[0]),
                AppContext.BaseDirectory)
        {
            _config = config;
        }

        public RemoteFileResolver(ImmutableArray<string> searchPaths, string? baseDirectory = null, IReadOnlyDictionary<string, string>? config = null)
        {
            _config = config;
            _fileBasedResolver = new SourceFileResolver(searchPaths, baseDirectory);
        }

        public override string? NormalizePath(string path, string? baseFilePath)
        {
            if (baseFilePath is null) return null;
            path = path.SimpleInject(_config);
            var uri = GetUri(path);
            if (uri == null) return _fileBasedResolver.NormalizePath(path, baseFilePath);

            return path;
        }

        public override Stream OpenRead(string resolvedPath)
        {
            var uri = GetUri(resolvedPath);
            if (uri == null) return _fileBasedResolver.OpenRead(resolvedPath);

            if (_remoteFiles.ContainsKey(resolvedPath))
            {
                var storedFile = _remoteFiles[resolvedPath];
                return new MemoryStream(Encoding.UTF8.GetBytes(storedFile));
            }

            return Stream.Null;
        }

        public override string? ResolveReference(string path, string? baseFilePath)
        {
            path = path.SimpleInject(_config);
            var uri = GetUri(path);
            if (uri == null) return _fileBasedResolver.ResolveReference(path, baseFilePath);

            var client = new HttpClient();
            var response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseFile = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrWhiteSpace(responseFile))
                {
                    _remoteFiles.Add(path, responseFile);
                }
            }
            return path;
        }

        private static Uri? GetUri(string input)
        {
            Uri? uriResult;
            if (Uri.TryCreate(input, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == "http"
                              || uriResult.Scheme == "https"))
            {
                return uriResult;
            }

            return null;
        }

        protected bool Equals(RemoteFileResolver other)
        {
            return Equals(_remoteFiles, other._remoteFiles) && Equals(_fileBasedResolver, other._fileBasedResolver);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RemoteFileResolver)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 37;
                hashCode = (hashCode * 397) ^ (_remoteFiles?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (_fileBasedResolver?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}