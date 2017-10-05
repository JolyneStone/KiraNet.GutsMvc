namespace KiraNet.GutsMVC.Implement
{
    public interface IActionInvokerProvider
    {
        void RegisterActionInvoker(IActionInvoker actionInvoker);
        IActionInvoker GetActionInvoker();
    }
}
