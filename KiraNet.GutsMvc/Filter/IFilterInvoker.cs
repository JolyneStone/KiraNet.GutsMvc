using KiraNet.GutsMvc.Implement;
using System;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.Filter
{
    public interface IFilterInvoker
    {
        Task FilterInvoke(ControllerContext controllerContext, Func<ControllerContext, Object[], Task> invokeAction);
    }
}
