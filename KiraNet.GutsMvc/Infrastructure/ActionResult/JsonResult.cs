using KiraNet.GutsMvc.Implement;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public class JsonResult : IActionResult
    {
        public object Data { get; set; }
        public string ContentType { get; set; }
        public Encoding ContentEncoding { get; set; }

        public void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = String.IsNullOrWhiteSpace(ContentType) ? "application/json" : ContentType;
            response.ContentEncoding = ContentEncoding ?? Encoding.UTF8;
            if (Data == null)
            {
                return;
            }

            var json = JsonConvert.SerializeObject(Data);
            var buffer = response.ContentEncoding.GetBytes(json);
            response.ResponseStream.Write(buffer, 0, buffer.Length);
        }

        public async Task ExecuteResultAsync(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = String.IsNullOrWhiteSpace(ContentType) ? "application/json" : ContentType;

            response.ContentEncoding = ContentEncoding ?? Encoding.UTF8;

            if (Data == null)
            {
                return;
            }

            var json = JsonConvert.SerializeObject(Data);
            var buffer = response.ContentEncoding.GetBytes(json);
            await response.ResponseStream.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
