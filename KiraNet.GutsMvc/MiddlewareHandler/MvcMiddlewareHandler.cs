using KiraNet.GutsMvc.Helper;
using KiraNet.GutsMvc.Implement;
using KiraNet.GutsMvc.View;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// GutsMvc的核心处理类
    /// </summary>
    internal sealed class MvcMiddlewareHandler : IMiddlewareHandle
    {
        public async Task MiddlewareExecute(HttpContext context)
        {
            var route = context.RouteEntity;
            if (route == null)
            {
                context.IsCancel = true;
                return;
            }

            await ExecuteMvc(context);
        }

        private async Task ExecuteMvc(HttpContext context)
        {

            var controllerContext = new ControllerContext(context);
            IControllerBulider controllerBuilder = new ControllerBuilder(controllerContext);

            try
            {
                var controller = controllerBuilder.ControllerBuild();
                await controller.Execute();
                controllerBuilder.ControllerRelease();
            }
            catch (NotFoundUrlException)
            {
                // not found case
                //var actionInvoker = new ActionInvokeProvider().GetActionInvoker();
                //actionInvoker.InvokeAction(controllerContext);
                //var path = Path.Combine(ViewPath.Path, "Shared", NotFoundUrlView.NotFoundViewName+ ".html");
                //if(!File.Exists(path))
                //{
                //    path = Path.Combine(ViewPath.Path, "Shared", NotFoundUrlView.NotFoundViewName + ".htm");
                //}

                if(!CheckFileExists(NotFoundUrlView.NotFoundViewName, ".html", out var path))
                {
                    if (!CheckFileExists(NotFoundUrlView.NotFoundViewName, ".htm", out path))
                    {
                        if (!CheckFileExists(NotFoundUrlView.NotFoundViewName, ".cshtml", out path))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            return;
                        }
                    }
                }

                var buffer = Encoding.UTF8.GetBytes(File.ReadAllText(path));
                context.Response.ContentType = "text/html";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ResponseStream.Write(buffer, 0, buffer.Length);           
            }
        }

        private static bool CheckFileExists(string viewName, string extension, out string path)
        {
            path = Path.Combine(ViewPath.Path, "Shared", NotFoundUrlView.NotFoundViewName + ".html");
            if (!File.Exists(path))
            {
                return false;
            }

            return true;
        }
    }
}
