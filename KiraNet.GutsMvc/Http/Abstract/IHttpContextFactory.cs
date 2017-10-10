using System;

namespace KiraNet.GutsMvc
{
    public interface IHttpContextFactory
    {
        HttpContext Create(IFeatureCollection featureCollection, IServiceProvider service, IServiceProvider serviceScope);
        void Dispose(HttpContext httpContext);
    }
}
