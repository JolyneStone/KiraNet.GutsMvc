using System;

namespace KiraNet.GutsMVC.ModelBinder
{
    public interface IModelBinderProvider
    {
        IModelBinder GetBinder(Type modelType);
    }
}