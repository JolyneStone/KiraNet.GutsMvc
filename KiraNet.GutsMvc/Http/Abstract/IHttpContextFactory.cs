using System;

namespace KiraNet.GutsMVC
{
    public interface IHttpContextFactory
    {
        HttpContext Create(IFeatureCollection featureCollection, IServiceProvider service, IServiceProvider serviceScope);
        void Dispose(HttpContext httpContext);
    }
}
