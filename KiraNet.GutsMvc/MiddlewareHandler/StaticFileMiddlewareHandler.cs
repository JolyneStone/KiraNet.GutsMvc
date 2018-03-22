using KiraNet.GutsMvc.Infrastructure;
using KiraNet.GutsMvc.StaticFiles;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    internal sealed class StaticFileMiddlewareHandler : IMiddlewareHandle
    {
        public async Task MiddlewareExecute(HttpContext httpContext)
        {
            if (!IsResourceRequest(httpContext.Request.RawUrl))
            {
                return;
            }
            var separator = Path.DirectorySeparatorChar;
            var path =
                    RootConfiguration.Root + separator +
                    httpContext.Request.RawUrl.ToString()
                    .Replace('/', separator);

            var file = new FileInfo(path);
            if (!file.Exists)
            {
                // 找不到文件
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                httpContext.Response.ResponseStream.Flush();
                httpContext.IsCancel = true;
                return;
            }

            if(!File.Exists(path))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                httpContext.IsCancel = true;
                return;
            }

            httpContext.Response.ContentType = GetContentType(path);
            byte[] contentBytes = File.ReadAllBytes(path);
            await httpContext.Response.ResponseStream.WriteAsync(contentBytes, 0, contentBytes.Length);

            httpContext.IsCancel = true;
        }

        private bool IsResourceRequest(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                return false;
            }

            var extension = Path.GetExtension(uri);

            return !String.IsNullOrWhiteSpace(extension) && !extension.Equals(".com", StringComparison.OrdinalIgnoreCase);
        }

        private string GetContentType(string path)
        {
            var extensionName = Path.GetExtension(path);
            IContentTypeMapping contentTypeMapping = new ContentTypeMapping();
            if (!contentTypeMapping.TryGetContentType(extensionName, out string contentType))
            {
                // 如果没有对应的ContentType，就使用.txt对应的ContentType
                contentType = "text/plain";
            }

            return contentType;
        }
    }
}
