using KiraNet.GutsMVC.Implement;
using Microsoft.Extensions.DependencyInjection;

namespace KiraNet.GutsMVC.ModelBinder
{
    public class ServiceModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object service;
            try
            {
                service = controllerContext.HttpContext.Service.GetService(bindingContext.ModelType);
            }
            catch
            {
                service = null;
            }

            if(service == null)
            {
                return null;
            }

            return service;
        }
    }
}
