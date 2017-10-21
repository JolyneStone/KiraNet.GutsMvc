using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.Infrastructure
{
    public sealed class FileCollectionValueProviderFactory : IValueProviderFactory
    {
        public IValueProvider CreateValueProvider(ControllerContext controllerContext)
        {
            if(controllerContext == null)
            {
                throw new ArgumentNullException(nameof(controllerContext));
            }

            try
            {
                if(controllerContext.HttpContext.Request.Form==null ||
                    controllerContext.HttpContext.Request.Form.Files==null)
                {
                    return null;
                }

                return new FileCollectionValueProvider(controllerContext.HttpContext.Request.Form.Files);
            }
            catch
            {
                return null;
            }
        }
    }
}
