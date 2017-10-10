using System;

namespace KiraNet.GutsMvc.ModelBinder
{
    public interface IModelBinderProvider
    {
        IModelBinder GetBinder(Type modelType);
    }
}