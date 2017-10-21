using System.Threading.Tasks;

namespace KiraNet.GutsMvc.Implement
{
    public interface IActionInvoker
    {
        void InvokeAction(ControllerContext controllerContext);
        Task InvokeActionAsync(ControllerContext controllerContext);
    }
}
