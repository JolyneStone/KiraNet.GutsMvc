using KiraNet.GutsMvc.Infrastructure;
using System.Reflection;

namespace KiraNet.GutsMvc.ModelBinder
{
    internal interface IModelBinderInvoker
    {
        bool TryBindModel(HttpContext httpContext, IValueProvider valueProvider, ParameterInfo parameter, out object value);
    }
}
