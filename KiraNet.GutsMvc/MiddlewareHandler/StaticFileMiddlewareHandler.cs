using KiraNet.GutsMvc.StaticFiles;
using System.IO;
using System.Net;

namespace KiraNet.GutsMvc
{
    internal sealed class StaticFileMiddlewareHandler : IMiddlewareHandle
    {
        public void MiddlewareExecute(HttpContext httpContext)
        {
            if (!IsResourceRequest(httpContext.Request.RawUrl))
            {
                return;
            }

            var path = (
                    Directory.GetCurrentDirectory() + @"\" +
                    httpContext.Request.RawUrl.ToString()
                    .Replace("/", @"\")
                    .Replace("//", @"\")
                ).Replace("\\\\", "\\");

            var file = new FileInfo(path);
            if (!file.Exists)
            {
                // 找不到文件
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                httpContext.Response.ResponseStream.Flush();
                httpContext.IsCancel = true;
                return;
            }

            //IStaticFileProvider fileProvider = new StaticFileProvider();
            //if (!fileProvider.TryGetFileStream(path, out var content))
            //{
            //    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            //    httpContext.IsCancel = true;
            //    return;
            //}

            //using (var writer = new StreamWriter(httpContext.Response.ResponseStream, Encoding.UTF8))
            //{
            //    writer.Write(content);
            //    httpContext.Response.StatusCode = (int)HttpStatusCode.Found;
            //    httpContext.Response.ContentType = GetContentType(path);
            //    //httpContext.Response.ResponseStream.Close();
            //}

            if(!File.Exists(path))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                httpContext.IsCancel = true;
                return;
            }

            httpContext.Response.ContentType = GetContentType(path);
            byte[] contentBytes = File.ReadAllBytes(path);
            httpContext.Response.ResponseStream.Write(contentBytes, 0, contentBytes.Length);

            httpContext.IsCancel = true;
        }

        private bool IsResourceRequest(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                return false;
            }

            return Path.HasExtension(uri);
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
