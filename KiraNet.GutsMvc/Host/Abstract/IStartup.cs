using Microsoft.Extensions.DependencyInjection;

namespace KiraNet.GutsMVC
{
    public interface IStartup
    {
        void Configure(IApplicationBuilder app);
        void ConfigureServices(IServiceCollection services);
    }
}
