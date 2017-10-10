namespace KiraNet.GutsMvc.Implement
{
    public interface IActionInvokerProvider
    {
        void RegisterActionInvoker(IActionInvoker actionInvoker);
        IActionInvoker GetActionInvoker();
    }
}
