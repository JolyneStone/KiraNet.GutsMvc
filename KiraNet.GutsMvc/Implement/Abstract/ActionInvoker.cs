using KiraNet.GutsMvc.Filter;
using KiraNet.GutsMvc.Implement;
using Microsoft.Extensions.DependencyInjection;


namespace KiraNet.GutsMvc
{
    public abstract class ActionInvoker : IActionInvoker
    {
        void IActionInvoker.InvokeAction(ControllerContext controllerContext)
        {
            IFilterInvoker filterInvoker = controllerContext.HttpContext.Service.GetRequiredService<IFilterInvoker>();
            filterInvoker.FilterInvoke(controllerContext, InvokeAction);
        }

        void IActionInvoker.InvokeActionAsync(ControllerContext controllerContext)
        {
            IFilterInvoker filterInvoker = controllerContext.HttpContext.Service.GetRequiredService<IFilterInvoker>();
            filterInvoker.FilterInvoke(controllerContext, InvokeActionAsync);
        }

        protected virtual void InvokeAction(ControllerContext controllerContext, object[] paramValues)
        {
        }

        protected virtual void InvokeActionAsync(ControllerContext controllerContext, object[] paramValues)
        {
        }
    }
}
