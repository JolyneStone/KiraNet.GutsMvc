namespace KiraNet.GutsMvc.ModelBinder
{
    public class ServiceModelBinder : IModelBinder
    {
        public object BindModel(HttpContext httpContext, ModelBindingContext bindingContext)
        {
            object service;
            try
            {
                service = httpContext.Service.GetService(bindingContext.ModelType);
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
