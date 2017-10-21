using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.Infrastructure
{
    public class RedirectQueryStringValueProviderFactory : IValueProviderFactory
    {
        public IValueProvider CreateValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException(nameof(controllerContext));
            }

            if (controllerContext.HttpContext.Request.RedirectQueryString == null)
            {
                return null;
            }

            return new RedirectQueryStringValueProvider(controllerContext);
        }
    }
}
