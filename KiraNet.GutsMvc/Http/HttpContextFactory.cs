using Microsoft.Extensions.DependencyInjection;
using System;

namespace KiraNet.GutsMvc
{
    public class HttpContextFactory : IHttpContextFactory
    {
        //private IHttpContextCache _contextCache;

        public HttpContext Create(IFeatureCollection featureCollection, IServiceProvider service, IServiceProvider serviceScope)
        {
            HttpContext httpContext =new DefaultHttpContext(featureCollection, service, serviceScope);

            // 由于IHttpContextCache我们采用了Singletion注入的方式，因此该_contextCache改变之后，所注入的IHttpContextCache也会改变
            //if (_contextCache == null)
            //    _contextCache = httpContext.Service.GetService<IHttpContextCache>();

            //_contextCache.HttpContext = httpContext;

            return httpContext;
        }

        public void Dispose(HttpContext httpContext)
        {
            httpContext = null;
            //if (_contextCache != null)
            //    _contextCache.HttpContext = null;
        }
    }
}
