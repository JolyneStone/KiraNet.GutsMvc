using Microsoft.Extensions.DependencyInjection;

namespace KiraNet.GutsMVC
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection ConfigureDefaultInject(this IServiceCollection services)
        {
            return services
                .AddSingleton<IApplicationBuilder, ApplicationBuilder>()
                .AddScoped<IHttpContextCache, HttpContextCache>();
        }
    }
}
