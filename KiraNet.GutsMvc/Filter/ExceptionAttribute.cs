using System;
using System.Text;

namespace KiraNet.GutsMvc.Filter
{
    public class ExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null && filterContext.Exception.GetType() == ExceptionType)
            {
                if (String.Equals(filterContext.HttpContext.Request.HttpMethod, "get", StringComparison.OrdinalIgnoreCase) &&
                    IsAjax == false)
                {
                    filterContext.Result = new ViewResult
                    {
                        FolderName = ControllerName,
                        ViewName = ActionName,
                        Model = filterContext.Exception
                    };
                }
                else
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = filterContext.Exception,
                        ContentEncoding = Encoding.UTF8
                    };
                }
            }

        }

        public bool IsAjax { get; set; }
        public Type ExceptionType { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}
