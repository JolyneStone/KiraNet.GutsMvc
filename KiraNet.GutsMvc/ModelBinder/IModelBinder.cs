namespace KiraNet.GutsMvc.ModelBinder
{
    public interface IModelBinder
    {
        object BindModel(HttpContext httpContext, ModelBindingContext bindingContext);
    }
}