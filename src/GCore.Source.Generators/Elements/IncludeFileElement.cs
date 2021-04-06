using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using GCore.Source.Attributes;
using GCore.Source.Extensions;
using GCore.Source.Generators.Attributes;

namespace GCore.Source.Generators.Elements
{
    [TaggedElement("IncludeFile")]
    public class IncludeFileElement : TaggedElement, IRawElement
    {
        [Config("FileUri")] public string FileUri { get; set; } = "";

        public string[] Lines { get; protected set; } = new string[0];

        public IncludeFileElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        public IncludeFileElement(SourceElement? parent, string name, string fileUri) : base(parent, name)
        {
            FileUri = fileUri;
            ReadFile();
        }

        protected void ReadFile()
        {
            var uri = new Uri(FileUri);

            if (uri.IsFile)
            {
                if (!File.Exists(uri.AbsolutePath))
                    throw new Exception("FilePath does not extist " + FileUri);

                SetLines(File.ReadAllLines(uri.AbsolutePath));
                return;
            }

            if (new string[] {"http", "https"}.Contains(uri.Scheme.ToLower()))
            {
                HttpClient client = new HttpClient();
                string result = client.GetStringAsync(uri).Result;
                SetLines(result.SplitNewLine());
                return;
            }

            if (new string[] {"ftp", "sftp", "ftps"}.Contains(uri.Scheme.ToLower()))
            {
                WebClient request = new WebClient();

                var userName = "anonymous";
                var password = "anonymous@anonymous.com";

                if (uri.UserInfo.Length > 0)
                {
                    var userInfoSplit = uri.UserInfo.Split(':');
                    userName = userInfoSplit[0];

                    if (userInfoSplit.Length > 1)
                        password = string.Join(":", userInfoSplit.Skip(1));
                }

                request.Credentials = new NetworkCredential(userName, password);

                byte[] fileData = request.DownloadData(uri);
                string result = System.Text.Encoding.UTF8.GetString(fileData);
                SetLines(result.SplitNewLine());
                return;
            }

            throw new Exception("Unknown Uri Schema: " + FileUri);
        }

        public void SetLines(IEnumerable<string> lines)
        {
            Lines = lines.ToArray();
        }

        public void SetLines(string lines)
        {
            SetLines(lines.SplitNewLine());
        }

        public override void Configure(IReadOnlyDictionary<string, string> config)
        {
            base.Configure(config);

            ReadFile();
        }

        public override void Render(CodeWriter writer)
        {
            if (!(_startLine is null) && RenderTags)
                writer.WriteLine(_startLine);

            writer.CurrentIndent += Indent;

            for (int i = 0; i < Lines.Length; i++) {
                writer.Write(Lines[i]);
                if (i < Lines.Length - 1)
                    writer.WriteLine();
            }

            if (Lines.Length > 0)
                writer.WriteLine();

            writer.CurrentIndent -= Indent;

            if (!(_stopLine is null) && RenderTags)
                writer.Write(_stopLine);
        }
    }
}