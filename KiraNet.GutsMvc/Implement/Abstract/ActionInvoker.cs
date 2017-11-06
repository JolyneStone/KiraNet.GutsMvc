using KiraNet.GutsMvc.Filter;
using KiraNet.GutsMvc.Implement;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public abstract class ActionInvoker : IActionInvoker
    {
        void IActionInvoker.InvokeAction(ControllerContext controllerContext)
        {
            IFilterInvoker filterInvoker = controllerContext.HttpContext.Service.GetRequiredService<IFilterInvoker>();
            filterInvoker.FilterInvoke(controllerContext, InvokeAction).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        async Task IActionInvoker.InvokeActionAsync(ControllerContext controllerContext)
        {
            IFilterInvoker filterInvoker = controllerContext.HttpContext.Service.GetRequiredService<IFilterInvoker>();
            await filterInvoker.FilterInvoke(controllerContext, InvokeActionAsync);
        }

        protected virtual async Task InvokeAction(ControllerContext controllerContext, object[] paramValues)
        {
            await Task.CompletedTask;
        }

        protected virtual async Task InvokeActionAsync(ControllerContext controllerContext, object[] paramValues)
        {
            await Task.CompletedTask;
        }
    }
}
