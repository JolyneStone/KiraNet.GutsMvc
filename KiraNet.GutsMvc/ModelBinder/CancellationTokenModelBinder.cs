using KiraNet.GutsMvc.Implement;
using System.Threading;

namespace KiraNet.GutsMvc.ModelBinder
{
    public class CancellationTokenModelBinder : IModelBinder
    {
        public object BindModel(HttpContext controllerContext, ModelBindingContext bindingContext)
        {
            return new CancellationToken();
        }
    }
}
