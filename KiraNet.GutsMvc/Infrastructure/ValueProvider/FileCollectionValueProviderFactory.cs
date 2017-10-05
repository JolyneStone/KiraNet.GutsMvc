using KiraNet.GutsMVC.Implement;
using System;

namespace KiraNet.GutsMVC.Infrastructure
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
                return new FileCollectionValueProvider(controllerContext.HttpContext.Request.Form.Files);
            }
            catch
            {
                return null;
            }
        }
    }
}
