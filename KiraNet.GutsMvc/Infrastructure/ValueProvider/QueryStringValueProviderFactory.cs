using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.Infrastructure
{
    public sealed class QueryStringValueProviderFactory : IValueProviderFactory
    {
        public IValueProvider CreateValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException(nameof(controllerContext));
            }

            if (controllerContext.HttpContext.Request.QueryString == null)
            {
                return null;
            }
            return new QueryStringValueProvider(controllerContext);
        }
    }
}
