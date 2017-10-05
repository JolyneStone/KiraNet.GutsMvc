using KiraNet.GutsMVC.Implement;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KiraNet.GutsMVC.View
{
    public class NVelocityViewEngine : IViewEngine
    {
        private object _sync = new object();
        public NVelocityViewEngine()
        {
        }

        public IView FindView(ControllerContext controllerContext, string folderName, string viewName)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                throw new System.ArgumentException("message", nameof(viewName));
            }
            if(String.IsNullOrWhiteSpace(folderName))
            {
                folderName = controllerContext.ControllerDescriptor.ControllerName;
            }
            if(String.IsNullOrWhiteSpace(viewName))
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

            view = new NVelocityView(folderName, viewName);

            lock (_sync)
            {
                viewCache.Set<IView>(key, view,
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)));
            }

            return view;
        }
    }
}