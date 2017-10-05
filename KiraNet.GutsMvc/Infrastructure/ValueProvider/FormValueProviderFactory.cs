using KiraNet.GutsMVC.Implement;
using System;

namespace KiraNet.GutsMVC.Infrastructure
{
    public sealed class FormValueProviderFactory : IValueProviderFactory
    {
        public IValueProvider CreateValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException(nameof(controllerContext));
            }

            try
            {
                return new FormValueProvider(controllerContext.HttpContext.Request.Form);
            }
            catch
            {
                return null;
            }
        }
    }
}
