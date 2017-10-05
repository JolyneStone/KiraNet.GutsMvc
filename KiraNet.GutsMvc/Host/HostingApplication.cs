using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KiraNet.GutsMVC
{
    /// <summary>
    /// 表示一个宿主应用，是对IHttpApplication的默认实现
    /// </summary>
    public class HostingApplication : IHttpApplication<Context>
    {
        public Func<HttpContext, Task> MiddlewareProcessor { get; }

        private IServiceProvider _service;
        /// <summary>
        /// 创建HostingApplication对象，在此之前需要将所有中间件的处理工作转换成一个RequestDelegate类型的委托，传入该构造函数中
        /// </summary>
        /// <param name="middlewaresProcessor"></param>
        public HostingApplication(Func<HttpContext, Task> middlewaresProcessor, IServiceProvider service)
        {
            MiddlewareProcessor = middlewaresProcessor;
            _service = service;
        }

        public Context CreateContext(IFeatureCollection contextFeatures)
        {
            var scope = _service.CreateScope();
            HttpContext httpContext = new HttpContextFactory().Create(contextFeatures, _service, scope.ServiceProvider);// new DefaultHttpContext(contextFeatures);

            return new Context
            {
                HttpContext = httpContext,
                StartTimestamp = System.Diagnostics.Stopwatch.GetTimestamp(),
                Scope = scope
            };
        }

        public void DisposeContext(Context context, Exception exception)
            => context.Scope?.Dispose();

        /// <summary>
        /// 将当前HTTP请求上下文通过由中间件构成的管道
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task ProcessRequestAsync(Context context)
        {
            try
            {
                return MiddlewareProcessor(context.HttpContext);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Expection: {ex.GetType().Name}:{ex.Message}");
                context.HttpContext.IsCancel = true;
                context.HttpContext.Response.StatusCode = 404;
                return Task.Run(() => { });
            }
        }
    }
}
