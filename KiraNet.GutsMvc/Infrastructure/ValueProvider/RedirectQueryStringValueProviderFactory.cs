using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.Infrastructure
{
    public class RedirectQueryStringValueProviderFactory : IValueProviderFactory
    {
        public IValueProvider CreateValueProvider(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (httpContext.Request.RedirectQueryString == null)
            {
                return null;
            }

            return new RedirectQueryStringValueProvider(httpContext);
        }
    }
}
