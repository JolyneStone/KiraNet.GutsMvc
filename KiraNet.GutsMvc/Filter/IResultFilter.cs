namespace KiraNet.GutsMvc.Filter
{
    public interface IResultFilter
    {
        //void OnResultExecuting(ResultExecutingContext filterContext);
        void OnResultExecuted(ResultExecutedContext filterContext);
    }
}
