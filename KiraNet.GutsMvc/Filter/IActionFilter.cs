namespace KiraNet.GutsMvc.Filter
{
    /// <summary>
    /// 动作过滤器
    /// </summary>
    public interface IActionFilter
    {
        void OnActionExecuting(ActionExecutingContext filterContext);
        void OnActionExecuted(ActionExecutedContext filterContext);
    }
}
