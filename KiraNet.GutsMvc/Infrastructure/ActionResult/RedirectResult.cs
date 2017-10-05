using KiraNet.GutsMVC.Implement;
using System;
using System.Net;
using System.Threading.Tasks;

namespace KiraNet.GutsMVC
{
    public class RedirectResult : IActionResult
    {
        public string Url { get; set; }

        public void ExecuteResult(ControllerContext context)
        {
            if(String.IsNullOrWhiteSpace(Url))
            {
                Url = context.HttpContext.Request.PathBase;
            }

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Redirect;
            context.HttpContext.Response.Redirect(Url);
        }

        public Task ExecuteResultAsync(ControllerContext context)
        {
            ExecuteResult(context);
            return Task.CompletedTask;
        }
    }
}
