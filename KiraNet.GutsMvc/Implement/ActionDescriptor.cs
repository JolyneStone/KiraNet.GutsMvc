using KiraNet.GutsMvc.Filter;
using KiraNet.GutsMvc.Helper;
using KiraNet.GutsMvc.Metadata;
using KiraNet.GutsMvc.ModelBinder;
using KiraNet.GutsMvc.ModelValid;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.Implement
{
    /// <summary>
    /// 用于描述Action
    /// </summary>
    public class ActionDescriptor:IFilterAttributeProvider
    {
        public string ActionName { get; set; }
        public MethodInfo Action { get; set; }
        public IServiceProvider Services { get; set; }
        public IEnumerable<FilterAttribute> GetFilterAttributes()
        {
            if(Action==null)
            {
                return null;
            }

            return Action.GetCustomAttributes<FilterAttribute>();
        }

        internal virtual ParameterDescriptor[] GetParameters(ControllerContext context)
        {
            if (Action == null)
            {
                return null;
            }

            var modelType = Action.GetCustomAttribute<ModelTypeAttribute>();
            if (modelType != null)
            {
                context.ModelType = modelType.ModelType ?? null;
            }

            return Action.GetParameters()
                .Select(x => new ParameterDescriptor { ParameterName = x.Name, ParameterType = x.ParameterType, ParameterInfo = x })
                .ToArray();
        }

        internal bool TryBindingParameter(Controller controller, out ParameterDescriptor[] paramDescriptors)
        {
            int index;
            paramDescriptors = GetParameters(controller.ControllerContext);

            if (paramDescriptors == null)
            {
                return false;
            }

            if (paramDescriptors.Length == 0)
            {
                controller.ControllerContext.ParameterDescriptors = null;
                return true;
            }

            for (index = 0; index < paramDescriptors.Length; index++)
            {
                var paramDescriptor = paramDescriptors[index];

                if (!String.IsNullOrWhiteSpace(controller.ControllerContext.RouteEntity.ParameterValue) &&
                    String.Equals(controller.ControllerContext.RouteEntity.DefaultParameter,
                    paramDescriptors[index].ParameterName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    if (controller.ControllerContext.RouteEntity.ParameterValue != null)
                    {
                        TypeConverter converter = TypeDescriptor.GetConverter(paramDescriptor.ParameterType);
                        try
                        {
                            var value = converter.ConvertTo(controller.ControllerContext.RouteEntity.ParameterValue, paramDescriptor.ParameterType);
                            paramDescriptors[index].ParameterValue = value;
                            continue;
                        }
                        catch
                        {

                        }
                    }
                }

                var modelBinder = ModelBinders.Binders.GetBinder(paramDescriptor.ParameterType);
                var modelMetadata = Services.GetRequiredService<IModelMetadataProvider>().GetMetadataForType(null, paramDescriptor.ParameterType);
                //var modelBinderModelBinders.Binders.GetBinder(paramDescriptor.ParameterType);

                if (TryBindModel(modelBinder, modelMetadata, controller, "", out var paramValue))
                {
                    paramDescriptors[index].ParameterValue = paramValue;
                    continue;
                }

                if (TryBindModel(modelBinder, modelMetadata, controller, paramDescriptor.ParameterName, out paramValue))
                {
                    paramDescriptors[index].ParameterValue = paramValue;
                    continue;
                }

                // 尝试依赖注入
                modelBinder = ModelBinders.Binders.GetBinder(typeof(IServiceProvider));

                if (TryBindModel(modelBinder, modelMetadata, controller, "", out paramValue))
                {

                    paramDescriptors[index].ParameterValue = paramValue;
                    continue;
                }

                if (DefaultParamterValue.TryGetDefaultValue(paramDescriptor.ParameterInfo, out paramValue))
                {
                    // 注：默认值可能为Null
                    paramDescriptors[index].ParameterValue = paramValue;
                    continue;
                }
                else
                {
                    break;
                }
            }

            if (index < paramDescriptors.Length)
            {
                return false;
            }

            controller.ModelState.ModelWrappers = paramDescriptors
                .Select(param => new ModelWrapper
                {
                    ModelName = param.ParameterName,
                    Model = param.ParameterValue,
                    ModelType = param.ParameterType
                });

            return true;
        }

        private bool TryBindModel(IModelBinder modelBinder, ModelMetadata modelMetadata, Controller controller, string modelName, out object value)
        {
            var bindingContext = new ModelBindingContext(controller.ValueProvider)
            {
                ModelName = modelName,
                ModelMetadata = modelMetadata,
            };

            object paramValue = modelBinder
                .BindModel(controller.ControllerContext, bindingContext);

            if (paramValue == null)
            {
                value = null;
                return false;
            }

            value = paramValue;
            return true;
        }
    }
}
