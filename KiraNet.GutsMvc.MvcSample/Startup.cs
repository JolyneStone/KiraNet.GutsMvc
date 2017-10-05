using Microsoft.Extensions.DependencyInjection;

namespace KiraNet.GutsMVC.MvcSample
{
    public class Startup : IStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            // 注册中间件
            //app.UseTest(@"C:\Users\99752\Pictures");
            app.UseGutsMVC(route=> 
                route.AddRouteMap("default", "/{controller=home}/{action=index}/{id}"))
            .ConfigureViews(@"D:\Code\KiraNet.GutsMVC\KiraNet.GutsMVC.MvcSample\Views");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
        }
    }
}
