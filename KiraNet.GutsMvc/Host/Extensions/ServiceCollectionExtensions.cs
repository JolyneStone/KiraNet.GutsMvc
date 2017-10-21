using KiraNet.GutsMvc.Filter;
using KiraNet.GutsMvc.Metadata;
using KiraNet.GutsMvc.ModelBinder;
using KiraNet.GutsMvc.ModelValid;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace KiraNet.GutsMvc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGutsMvc(this IServiceCollection services)
        {
            return services
                .AddMemoryCache()
                .AddSingleton<IApplicationBuilder, ApplicationBuilder>()
                .AddSingleton<ModelValidator, DefaultModelValidator>()
                .AddScoped<IModelState, ModelState>(_ => new ModelState(_))
                .AddScoped<IModelMetadataProvider, DefaultModelMetadataProvider>()
                .AddScoped<IModelBinderProvider, ModelBinderDictionary>(_ => new ModelBinderDictionary()
                {
                    {typeof(IFormFile), new FileModelBinder() },
                    {typeof(byte[]), new ByteArrayModelBinder() },
                    {typeof(CancellationToken), new CancellationTokenModelBinder() },
                    {typeof(IModelBinder), new OrdinaryModelBinder(_) },
                    {typeof(IServiceProvider), new ServiceModelBinder() }
                })
                .AddSingleton<IFilterProvider, FilterProviderCollection>(_ => new FilterProviderCollection
                        {
                            GlobalFilterCollection.GlobalFilter,
                            new FilterAttributeFilterProvider(),
                            new ControllerFilterProvider()
                        }
                )
                .AddSingleton<IClaimSchema, ClaimSchema>()
                .AddSingleton<IFilterInvoker, FilterInvoker>();
        }

        public static IServiceCollection AddAutoInJection(this IServiceCollection services, Assembly assembly)
        {
            if (assembly == null)
            {
                return services;
            }

            var dependencyAttributeType = typeof(DependencyInjectionAttribute);
            var dependencies = assembly.GetTypes()
                .Where(x => x.IsDefined(dependencyAttributeType))
                .Select(x =>
                {
                    var depency = x.GetCustomAttribute<DependencyInjectionAttribute>();
                    if (depency.ImplementType == null)
                    {
                        depency.ImplementType = x;
                    }

                    return depency;
                });

            foreach (var transient in dependencies.Where(x => 
                x.Dependency == DependencyType.Transient))
            {
                services.AddTransient(transient.ServiceType, transient.ImplementType);
            }

            foreach (var scoped in dependencies.Where(x => 
                x.Dependency == DependencyType.Scoped))
            {
                services.AddScoped(scoped.ServiceType, scoped.ImplementType);
            }

            foreach (var singleton in dependencies.Where(x =>
                x.Dependency == DependencyType.Singleton))
            {
                services.AddSingleton(singleton.ServiceType, singleton.ImplementType);
            }

            return services;
        }
    }
}
