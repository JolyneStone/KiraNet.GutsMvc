using KiraNet.GutsMVC.Implement;
using System;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMVC
{
    public class ContentResult : IActionResult
    {
        public string Content { get; set; }
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public void ExecuteResult(ControllerContext context)
        {
            Execute(context);
        }

        public Task ExecuteResultAsync(ControllerContext context)
        {
            Execute(context);
            return Task.CompletedTask;
        }

        private void Execute(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }


            HttpResponse response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Content != null)
            {
                var buffer = ContentEncoding.GetBytes(Content);
                response.ResponseStream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
