using KiraNet.GutsMvc.Implement;
using System;
using System.Net;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public class HttpStatusCodeResult : IActionResult
    {
        public int StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.HttpContext.Response.StatusCode = StatusCode;
            if (StatusDescription != null)
            {
                context.HttpContext.Response.StatusDescription = StatusDescription;
            }
        }

        public Task ExecuteResultAsync(ControllerContext context)
        {
            ExecuteResult(context);
            return Task.CompletedTask;
        }
    }
}