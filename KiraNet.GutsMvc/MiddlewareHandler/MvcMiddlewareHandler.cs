using KiraNet.GutsMvc.Helper;
using KiraNet.GutsMvc.Implement;
using KiraNet.GutsMvc.View;
using System;
using System.IO;
using System.Net;
using System.Reflection;
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
                // CookieCollection对Cookie的处理有一个bug，
                // 当设置多个cookie的时候，Response的headers只有一个Set-Cookie
                // 且多个cookie的值都在这个Set-Cookie中
                // 所以我们需要自己对cookie进行处理
                CookieFix(context.Response);
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

                if (!CheckFileExists(NotFoundUrlView.NotFoundViewName, ".html", out var path))
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

        private static void CookieFix(HttpResponse response)
        {
            if (response == null)
            {
                return;
            }

            if (response.Cookies.Count == 0)
            {
                return;
            }

            //var method = typeof(Cookie).GetMethod("ToServerString", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance);
            response.Headers.Remove(HttpResponseHeader.SetCookie);
            for(var i = 0; i<response.Cookies.Count;i++)
            {
                var cookie = response.Cookies[i];
                //var str = (string)method.Invoke(cookie, null);
                //response.Headers.Add(HttpResponseHeader.SetCookie, str);
                //continue;
                string cookieStr = $"{cookie.Name}={cookie.Value}";
                if(!String.IsNullOrWhiteSpace(cookie.Comment))
                {
                    cookieStr = $"{cookieStr}; comment={cookie.Comment}";
                }
                else if(null!=cookie.CommentUri)
                {
                    cookieStr = $"{cookieStr}; comment={cookie.CommentUri.ToString()}";
                }

                if (!String.IsNullOrWhiteSpace(cookie.Domain))
                {
                    cookieStr = $"{cookieStr}; domain={cookie.Domain}";
                }

                if (cookie.Expires != null && !cookie.Expired)
                {
                    //cookieStr = $"{cookieStr}; expires={cookie.Expires.ToString("ddd, dd-MMM-yyyy HH:mm:ss") + " GMT"}";
                    cookieStr = $"{cookieStr}; expires={cookie.Expires.ToString("r")}";
                    if (cookie.Expires > DateTime.Now)
                    {
                        var subTime = cookie.Expires - DateTime.Now;
                        cookieStr = $"{cookieStr}; max-age={(long)subTime.TotalSeconds}";
                    }
                }

                if (!String.IsNullOrWhiteSpace(cookie.Path))
                {
                    cookieStr = $"{cookieStr}; path={cookie.Path}";
                }

                if(cookie.HttpOnly)
                {
                    cookieStr = $"{cookieStr}; httponly";
                }

                if(cookie.Secure)
                {
                    cookieStr = $"{cookieStr}; secure";
                }

              response.Headers.Add(HttpResponseHeader.SetCookie, cookieStr);
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
