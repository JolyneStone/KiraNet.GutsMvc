using System;
using System.Threading;

namespace KiraNet.GutsMvc.ModelBinder
{
    public static class ModelBinders
    {
        private static readonly ModelBinderDictionary _binders = new ModelBinderDictionary()
        {
            {typeof(IFormFile), new FileModelBinder() },
            {typeof(byte[]), new ByteArrayModelBinder() },
            {typeof(CancellationToken), new CancellationTokenModelBinder() },
            {typeof(IModelBinder), new OrdinaryModelBinder() },
            {typeof(IServiceProvider), new ServiceModelBinder() }
        };

        public static ModelBinderDictionary Binders => _binders;

        //public static IModelBinder GetBinderFromAttributes(Type type)
        //{
        //    var attributes = type.GetCustomAttributes(true).OfType<Attribute>();


        //}
    }
}