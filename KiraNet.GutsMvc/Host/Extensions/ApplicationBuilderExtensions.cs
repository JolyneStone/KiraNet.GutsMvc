using KiraNet.GutsMVC.Route;
using System;
using System.Threading.Tasks;

namespace KiraNet.GutsMVC
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSessionExpireTime(this IApplicationBuilder app, TimeSpan expireTime)
        {
            SessionExpireTimeConfigure.ExpireConfigure = expireTime;
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

                    middlewareHandle.MiddlewareExecute(context);
                };
            };

            return app.Use(middleware);
        }

        public static IApplicationBuilder UseGutsMVC(this IApplicationBuilder app, Action<RouteConfiguration> routeConfiguration)
        {
            if (routeConfiguration == null)
            {
                throw new ArgumentNullException(nameof(routeConfiguration));
            }

            // 注册路由
            routeConfiguration(RouteConfiguration.RouteConfig);

            return app.Use(new MvcHandler());
        }

        public static IApplicationBuilder ConfigureViews(this IApplicationBuilder app, string viewPath)
        {
            new View.ViewPath(viewPath);
            return app;
        }
    }
}
