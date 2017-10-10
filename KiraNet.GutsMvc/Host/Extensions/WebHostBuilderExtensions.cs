using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using KiraNet.GutsMvc.Helper;

namespace KiraNet.GutsMvc
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder Configure(this IWebHostBuilder builder, Action<IServiceCollection, IApplicationBuilder> configure)
            => builder.ConfigureServices(services =>
            services.AddSingleton<IApplicationStartup>(_ => new ApplicationStartup(services, configure)));

        public static IWebHostBuilder UseHttpListener(this IWebHostBuilder builder)
        {
            return builder.ConfigureServices(services =>
            services.AddSingleton<IServer, HttpListenerServer>(_ => new HttpListenerServer(services)));
        }

        public static IWebHostBuilder UseUrls(this IWebHostBuilder builder, params string[] urls)
            => builder.UseSetting("ServerAddresses", String.Join(";", urls));

        public static IWebHostBuilder UseStartup<TStartup>(this IWebHostBuilder builder) where TStartup : class, IStartup
        {
            return builder.UseStartup(typeof(TStartup));
        }

        public static IWebHostBuilder UseStartup(this IWebHostBuilder builder, Type startupType)
        {
            var typeInfo = startupType.GetTypeInfo();
            var constructors = typeInfo.GetConstructors()
                .Where(x => x.IsPublic)
                .OrderBy(x => x.GetParameters().Length);

            IStartup startup = null;
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                var objParams = new object[parameters.Length];
                int i = 0;
                for (i = 0; i < parameters.Length; i++)
                {
                    if (DefaultParamterValue.TryGetDefaultValue(parameters[i], out var value))
                    {
                        objParams[i] = value;
                        var x = new ConfigurationBuilder();
                    }
                    else
                    {
                        break;
                    }
                }

                if (i < parameters.Length)
                    continue;

                startup = (IStartup)constructor.Invoke(objParams);
                break;
            }

            if (startup == null)
                throw new InvalidOperationException("操作失败，无法构造出Startup类");

            return builder.Configure((services, app) =>
            {
                startup.ConfigureServices(services);
                startup.Configure(app);
            });
        }
    }
}
