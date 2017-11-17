using KiraNet.GutsMvc.WebSocketHub;
using KiraNet.GutsMvc.Implement;
using KiraNet.GutsMvc.MiddlewareHandler;
using KiraNet.GutsMvc.Route;
using KiraNet.GutsMvc.View;
using System;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSessionExpireTime(this IApplicationBuilder app, TimeSpan expireTime)
        {
            SessionExpireTimeConfigure.ExpireConfigure = expireTime;
            return app;
        }

        public static IApplicationBuilder UseEngine(this IApplicationBuilder app, ViewEngine viewEngine)
        {
            if (viewEngine != null)
            {
                ViewEngine.Current = viewEngine;
            }

            return app;
        }

        public static IApplicationBuilder Use(this IApplicationBuilder app, IMiddlewareHandle middlewareHandle)
        {
            if (middlewareHandle == null)
            {
                throw new ArgumentNullException(nameof(middlewareHandle));
            }

            Func<Func<HttpContext, Task>, Func<HttpContext, Task>> middleware = last =>
            {
                return async context =>
                {
                    await last(context);

                    if (context.IsCancel)
                        return;

                    await middlewareHandle.MiddlewareExecute(context);
                };
            };

            return app.Use(middleware);
        }

        /// <summary>
        /// 注册GutsMvc中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="routeConfiguration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGutsMvc(this IApplicationBuilder app, Action<RouteConfiguration> routeConfiguration)
        {
            if (routeConfiguration == null)
            {
                throw new ArgumentNullException(nameof(routeConfiguration));
            }

            // 注册路由
            routeConfiguration(RouteConfiguration.RouteConfig);

            // 设置视图引擎，默认为Razor视图引擎
            if (ViewEngine.Current == null)
            {
                ViewEngine.Current = new RazorViewEngine();
            }

            return app.Use(new MvcMiddlewareHandler());
        }

        /// <summary>
        /// 注册针对WebSocket处理的中间件
        /// 注：最好将此方法在UserGutsMvc()方法之前调用
        /// </summary>
        /// <param name="app"></param>
        /// <param name="hubConfiguration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UserWebSocketHub(this IApplicationBuilder app, Action<HubMap> hubConfiguration)
        {
            if (hubConfiguration == null)
            {
                throw new ArgumentNullException(nameof(hubConfiguration));
            }

            hubConfiguration(HubMap.Map);

            return app.Use(new WebSocketHubHandler());
        }

        public static IApplicationBuilder ConfigureView(this IApplicationBuilder app, string viewPath)
        {
            new View.ViewPath(viewPath);
            return app;
        }

        public static IApplicationBuilder ConfigureNotFoundView(this IApplicationBuilder app, string viewName)
        {
            if (String.IsNullOrWhiteSpace(viewName))
            {
                NotFoundUrlView.NotFoundViewName = viewName;
            }

            return app;
        }
    }
}
