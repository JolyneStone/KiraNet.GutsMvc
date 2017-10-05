using KiraNet.GutsMVC.Implement;

namespace KiraNet.GutsMVC.ModelBinder
{
    public interface IModelBinder
    {
        object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);
    }
}