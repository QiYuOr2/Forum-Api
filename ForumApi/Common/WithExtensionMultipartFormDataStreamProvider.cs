using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ForumApi.Common
{
    /// <summary>
    /// 处理图片名
    /// </summary>
    public class WithExtensionMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public Guid Guid { get; set; }

        public WithExtensionMultipartFormDataStreamProvider(string rootPath, Guid guid)
            : base(rootPath)
        {
            Guid = guid;
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            string extension =
                !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ?
                Path.GetExtension(GetValidFileName(headers.ContentDisposition.FileName)) : "";

            return Guid + extension;
        }

        private string GetValidFileName(string filePath)
        {
            char[] invalids = Path.GetInvalidFileNameChars();
            return string.Join("_", filePath.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
        }
    }
}