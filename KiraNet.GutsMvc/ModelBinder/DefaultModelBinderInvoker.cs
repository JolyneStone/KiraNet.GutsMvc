using KiraNet.GutsMvc.Helper;
using KiraNet.GutsMvc.Infrastructure;
using KiraNet.GutsMvc.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Reflection;

namespace KiraNet.GutsMvc.ModelBinder
{
    internal class DefaultModelBinderInvoker : IModelBinderInvoker
    {
        public bool TryBindModel(HttpContext httpContext, IValueProvider valueProvider, ParameterInfo parameter, out object value)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (valueProvider == null)
            {
                throw new ArgumentNullException(nameof(valueProvider));
            }

            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            value = null;
            if (!String.IsNullOrWhiteSpace(httpContext.RouteEntity.ParameterValue) &&
                String.Equals(httpContext.RouteEntity.DefaultParameter,
                 parameter.Name,
                StringComparison.OrdinalIgnoreCase))
            {
                if (httpContext.RouteEntity.ParameterValue != null)
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(parameter.ParameterType);
                    try
                    {
                        value = converter.ConvertTo(httpContext.RouteEntity.ParameterValue, parameter.ParameterType);
                        return true;
                    }
                    catch
                    {

                    }
                }
            }

            var binderProviders = httpContext.Service.GetRequiredService<IModelBinderProvider>();
            var modelBinder = binderProviders.GetBinder(parameter.ParameterType);
            var modelMetadata = httpContext.Service.GetRequiredService<IModelMetadataProvider>().GetMetadataForType(null, parameter.ParameterType);
            //var modelBinderModelBinders.Binders.GetBinder(paramDescriptor.ParameterType);

            if (TryBindValue(modelBinder, modelMetadata, httpContext, valueProvider, "", out var paramValue))
            {
                value = paramValue;
                return true;
            }

            if (TryBindValue(modelBinder, modelMetadata, httpContext, valueProvider, parameter.Name, out paramValue))
            {
                value = paramValue;
                return true;
            }

            // 尝试依赖注入
            modelBinder = binderProviders.GetBinder(typeof(IServiceProvider));

            if (TryBindValue(modelBinder, modelMetadata, httpContext, valueProvider, "", out paramValue))
            {
                value = paramValue;
                return true;
            }

            if (DefaultParamterValue.TryGetDefaultValue(parameter, out paramValue))
            {
                // 注：默认值可能为Null
                value = paramValue;
                return true;
            }
            else if (TypeHelper.IsNullableValueType(modelMetadata.ModelType))
            {
                value = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TryBindValue(
            IModelBinder modelBinder,
            ModelMetadata modelMetadata,
            HttpContext httpContext,
            IValueProvider valueProvider,
            string modelName,
            out object value)
        {
            try
            {
                var bindingContext = new ModelBindingContext(valueProvider)
                {
                    ModelName = modelName,
                    ModelMetadata = modelMetadata,
                };

                value = modelBinder
                    .BindModel(httpContext, bindingContext);

                if (value == null)
                {
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                value = null;
                return false;
            }
        }
    }
}
