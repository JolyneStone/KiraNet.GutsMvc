using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.Infrastructure
{
    public sealed class FormValueProviderFactory : IValueProviderFactory
    {
        public IValueProvider CreateValueProvider(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            try
            {
                if(httpContext.Request.Form == null)
                {
                    return null;
                }

                return new FormValueProvider(httpContext.Request.Form);
            }
            catch
            {
                return null;
            }
        }
    }
}
