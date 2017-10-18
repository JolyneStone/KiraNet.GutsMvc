using KiraNet.GutsMvc.Filter;
using KiraNet.GutsMvc.MvcSample.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace KiraNet.GutsMvc.MvcSample
{
    public class Startup : IStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            // 注册中间件
            app.UseGutsMvc(route =>
                route.AddRouteMap("default", "/{controller=home}/{action=index}/{id}"));
            //.ConfigureViews(@"D:\Code\KiraNet.GutsMvc\KiraNet.GutsMvc.MvcSample");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // 增加对GutsMvc的支持
            services.AddGutsMvc();
            services.AddSingleton<IClaimSchema, ClaimShemeSample>();
        }
    }
}
