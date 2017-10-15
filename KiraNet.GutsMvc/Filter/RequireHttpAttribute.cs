using System;

namespace KiraNet.GutsMvc.Filter
{
    public class RequireHttpAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if(!filterContext.HttpContext.Request.IsSecureConnection)
            {
                if (String.Equals(filterContext.HttpContext.Request.HttpMethod, "get", StringComparison.OrdinalIgnoreCase))
                {
                    var url = filterContext.HttpContext.Request.Url.ToString().Replace("http://", "https://");

                    filterContext.Result = new RedirectResult
                    {
                        Url = url
                    };
                }
                else
                {
                    throw new InvalidOperationException("非Https请求！");
                }
            }
        }
    }
}
