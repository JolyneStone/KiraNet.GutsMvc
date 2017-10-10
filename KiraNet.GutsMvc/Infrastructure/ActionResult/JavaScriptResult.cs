using KiraNet.GutsMvc.Implement;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public class JavaScriptResult : IActionResult
    {
        public string Script { get; set; }
        public void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/x-javascript";
            if (Script != null)
            {
                var buffer = Encoding.UTF8.GetBytes(Script);
                context.HttpContext.Response.ResponseStream.Write(buffer, 0, buffer.Length);
            }
        }

        public async Task ExecuteResultAsync(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/x-javascript";
            if (Script != null)
            {
                var buffer = Encoding.UTF8.GetBytes(Script);
                await context.HttpContext.Response.ResponseStream.WriteAsync(buffer, 0, buffer.Length);
            }
        }
    }
}
