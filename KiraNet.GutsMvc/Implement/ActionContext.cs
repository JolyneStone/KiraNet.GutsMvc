using KiraNet.GutsMvc.Route;
using System;

namespace KiraNet.GutsMvc.Implement
{
    /// <summary>
    /// 执行action的上下文对象
    /// </summary>
    public class ActionContext
    {
        public RouteEntity RouteEntity { get; set; }
        public HttpContext HttpContext { get; set; }

        public ActionContext() { }

        public ActionContext(ActionContext actionContext) : this(actionContext.HttpContext, actionContext.RouteEntity)
        {
            if (actionContext == null)
                throw new ArgumentNullException(nameof(actionContext));
        }

        public ActionContext(
            HttpContext httpContext,
            RouteEntity routeEntity)
        {
            HttpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            RouteEntity = routeEntity ?? throw new ArgumentNullException(nameof(httpContext));
        }
    }
}
