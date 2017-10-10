using KiraNet.GutsMvc.Helper;
using KiraNet.GutsMvc.Implement;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KiraNet.GutsMvc.View
{
    public abstract class ViewEngine : IViewEngine
    {
        private static IViewEngine _current;
        private static KiraSpinLock _sync = new KiraSpinLock();
        public static IViewEngine Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new RazorViewEngine();
                }

                return _current;
            }
            internal set
            {
                if (value != null)
                {
                    _current = value;
                }
            }
        }

        public IView CreateView(ControllerContext controllerContext, string folderName, string viewName)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                throw new System.ArgumentException("message", nameof(viewName));
            }
            if (String.IsNullOrWhiteSpace(folderName))
            {
                folderName = controllerContext.ControllerDescriptor.ControllerName;
            }
            if (String.IsNullOrWhiteSpace(viewName))
            {
                viewName = controllerContext.ActionDescriptor.ActionName;
            }

            var key = new ViewCacheKey(controllerContext.ControllerDescriptor.ControllerName, viewName);
            IMemoryCache viewCache = controllerContext.HttpContext.Service.GetRequiredService<IMemoryCache>();
            if (viewCache.TryGetValue<IView>(key, out var view))
            {
                if (view != null)
                {
                    return view;
                }
            }

            view = CreateView(folderName, viewName);

            _sync.Enter();
            viewCache.Set<IView>(key, view,
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)));
            _sync.Exit();

            return view;
        }

        public abstract IView CreateView(string folderName, string viewName);
    }
}
