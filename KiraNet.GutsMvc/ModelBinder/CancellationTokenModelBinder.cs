using KiraNet.GutsMVC.Implement;
using System.Threading;

namespace KiraNet.GutsMVC.ModelBinder
{
    public class CancellationTokenModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return new CancellationToken();
        }
    }
}
