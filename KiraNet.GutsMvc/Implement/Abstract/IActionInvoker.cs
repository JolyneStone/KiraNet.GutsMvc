using System.Threading.Tasks;

namespace KiraNet.GutsMVC.Implement
{
    public interface IActionInvoker
    {
        void InvokeAction(ControllerContext controllerContext);
        void InvokeActionAsync(ControllerContext controllerContext);
    }
}
