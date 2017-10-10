using Microsoft.Extensions.DependencyInjection;

namespace KiraNet.GutsMvc
{
    public interface IStartup
    {
        void Configure(IApplicationBuilder app);
        void ConfigureServices(IServiceCollection services);
    }
}
