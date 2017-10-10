using KiraNet.GutsMvc.Implement;
using System.Threading;

namespace KiraNet.GutsMvc.ModelBinder
{
    public class CancellationTokenModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return new CancellationToken();
        }
    }
}
