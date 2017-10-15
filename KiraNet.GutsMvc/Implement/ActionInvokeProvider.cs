namespace KiraNet.GutsMvc.Implement
{
    public class ActionInvokeProvider : IActionInvokerProvider
    {
        private static IActionInvoker _actionInvoker;

        public virtual void RegisterActionInvoker(IActionInvoker actionInvoker = null)
        {
            if (actionInvoker == null && _actionInvoker == null)
            {
                _actionInvoker = new ExpressionActionInvoker();
            }

            _actionInvoker = actionInvoker;
        }

        public virtual IActionInvoker GetActionInvoker()
        {
            if (_actionInvoker == null)
            {
                _actionInvoker = new ExpressionActionInvoker();
            }

            return _actionInvoker;
        }
    }
}
