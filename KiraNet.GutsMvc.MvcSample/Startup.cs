using Microsoft.Extensions.DependencyInjection;

namespace KiraNet.GutsMvc.MvcSample
{
    public class Startup : IStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            // 注册中间件
            //app.UseTest(@"C:\Users\99752\Pictures");
            app.UseGutsMvc(route =>
                route.AddRouteMap("default", "/{controller=home}/{action=index}/{id}"));
            //.ConfigureViews(@"D:\Code\KiraNet.GutsMvc\KiraNet.GutsMvc.MvcSample");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
        }
    }
}
