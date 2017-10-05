using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMVC.MvcSample
{
    public static class WebHostBuilderExtensions
    {
        private static Dictionary<string, string> mediaTypeMappings = new Dictionary<string, string>(System.StringComparer.OrdinalIgnoreCase);

        static WebHostBuilderExtensions()
        {
            mediaTypeMappings.Add(".jpg", "image/jpeg");
            mediaTypeMappings.Add(".gif", "image/gif");
            mediaTypeMappings.Add(".png", "image/png");
            mediaTypeMappings.Add(".bmp", "image/bmp");
        }

        public static IApplicationBuilder UseTest(this IApplicationBuilder app, string rootDirectory)
        {
            Func<Func<HttpContext, Task >, Func<HttpContext, Task>> middleware = last =>
            {
                return async context =>
                {
                    await last(context);

                    if (context.IsCancel)
                        return;
                    var a = context.Request.QueryString;
                    var id = a["id"];
                    var user = a["user"];
                    var p1 = context.Request.RawUrl; // /Test?id=1&user=zzq
                    var p2 = context.Request.Url;    // http://localhost:17758/Test?id=1&user=zzq
                    var p3 = context.Request.PathBase; // /
                   
                    var baseDirectory = AppContext.BaseDirectory;

                    string urlPath = context.Request.Url.LocalPath
                    .Substring(context.Request.PathBase.Length);
                    var filePath = Path.Combine(rootDirectory, urlPath)
                    .Replace('/', Path.DirectorySeparatorChar);

                    filePath = File.Exists(filePath) ?
                    filePath :
                    Directory.GetFiles(Path.GetDirectoryName(filePath))
                        .FirstOrDefault(x => String.Compare(Path.GetFileNameWithoutExtension(x), Path.GetFileName(filePath), true) == 0);

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        string extension = Path.GetExtension(filePath);
                        if (mediaTypeMappings.TryGetValue(extension, out string mediaType))
                        {
                            await context.Response.WriteFileAsync(filePath, mediaType);
                        }
                        else
                        {
                            await context.Response.WriteFileAsync(filePath, "text/html");
                        }
                    }
                    else
                    {
                        var buf = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new { msg = "Test" }));
                        context.Response.ContentType = "application/json";
                        await context.Response.ResponseStream.WriteAsync(buf, 0, buf.Length);
                    }

                    context.IsCancel = false;
                };
            };

            return app.Use(middleware);
        }

        public static async Task WriteFileAsync(this HttpResponse response, string fileName, string contentType)
        {
            if (File.Exists(fileName))
            {
                byte[] content = File.ReadAllBytes(fileName);
                response.ContentType = contentType;
                await response.ResponseStream.WriteAsync(content, 0, content.Length);
            }
            response.StatusCode = 200;
        }
    }
}
