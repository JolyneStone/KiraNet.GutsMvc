using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.Filter
{
    public interface IFilterInvoker
    {
        void FilterInvoke(ControllerContext controllerContext, Action<ControllerContext, Object[]> invokeAction);
    }
}
