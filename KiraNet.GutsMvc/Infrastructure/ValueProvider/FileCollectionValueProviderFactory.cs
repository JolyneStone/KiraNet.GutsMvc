using System;

namespace KiraNet.GutsMvc.Infrastructure
{
    public sealed class FileCollectionValueProviderFactory : IValueProviderFactory
    {
        public IValueProvider CreateValueProvider(HttpContext httpContext)
        {
            if(httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            try
            {
                if(httpContext.Request.Form==null ||
                    httpContext.Request.Form.Files==null)
                {
                    return null;
                }

                return new FileCollectionValueProvider(httpContext.Request.Form.Files);
            }
            catch
            {
                return null;
            }
        }
    }
}
