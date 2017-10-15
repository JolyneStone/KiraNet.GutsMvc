using KiraNet.GutsMvc.Filter;
using KiraNet.GutsMvc.Metadata;
using KiraNet.GutsMvc.ModelBinder;
using KiraNet.GutsMvc.ModelValid;
using Microsoft.Extensions.DependencyInjection;
using System;
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
                .AddScoped<IModelState, ModelState>()
                .AddScoped<IModelMetadataProvider, DefaultModelMetadataProvider>()
                .AddSingleton<IModelBinderProvider, ModelBinderDictionary>(_ => new ModelBinderDictionary()
                {
                    {typeof(IFormFile), new FileModelBinder() },
                    {typeof(byte[]), new ByteArrayModelBinder() },
                    {typeof(CancellationToken), new CancellationTokenModelBinder() },
                    {typeof(IModelBinder), new OrdinaryModelBinder() },
                    {typeof(IServiceProvider), new ServiceModelBinder() }
                })
                .AddSingleton<IFilterProvider, FilterProviderCollection>(_ => new FilterProviderCollection
                        {
                            GlobalFilterCollection.GlobalFilter,
                            new FilterAttributeFilterProvider(),
                            new ControllerFilterProvider()
                        }
                )
                .AddSingleton<IClaimSchema, ClaimSchema>();
        }
    }
}
