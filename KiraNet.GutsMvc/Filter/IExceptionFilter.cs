namespace KiraNet.GutsMvc.Filter
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public interface IExceptionFilter
    {
        void OnException(ExceptionContext filterContext);
    }
}
