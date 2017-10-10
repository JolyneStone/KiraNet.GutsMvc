using KiraNet.GutsMvc.Implement;

namespace KiraNet.GutsMvc.ModelBinder
{
    public interface IModelBinder
    {
        object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);
    }
}