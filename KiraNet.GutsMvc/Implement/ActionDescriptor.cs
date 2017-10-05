using KiraNet.GutsMVC.Helper;
using KiraNet.GutsMVC.Metadata;
using KiraNet.GutsMVC.ModelBinder;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace KiraNet.GutsMVC.Implement
{
    /// <summary>
    /// 用于描述Action
    /// </summary>
    public class ActionDescriptor
    {
        //private IList<ParameterDescriptor> _parameters;
        public string ActionName { get; set; }
        public MethodInfo Action { get; set; }

        internal virtual ParameterDescriptor[] GetParameters(ControllerContext context)
        {
            if (Action == null)
            {
                return null;
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
                // TODO: 没有正常得到Model元数据
                var modelMetadata = DefaultModelMetadataProvider.Current.GetMetadataForType(null, paramDescriptors[index].ParameterType);
                var bindingContext = new ModelBindingContext(controller.ValueProvider)
                {
                    ModelName = paramDescriptors[index].ParameterName,
                    ModelMetadata = modelMetadata,
                };

                if (!String.IsNullOrWhiteSpace(controller.ControllerContext.RouteEntity.ParameterValue) &&
                    String.Equals(controller.ControllerContext.RouteEntity.DefaultParameter,
                    paramDescriptors[index].ParameterName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    if (controller.ControllerContext.RouteEntity.ParameterValue != null)
                    {
                        TypeConverter converter = TypeDescriptor.GetConverter(paramDescriptors[index].ParameterType);
                        try
                        {
                            var value = converter.ConvertTo(controller.ControllerContext.RouteEntity.ParameterValue, paramDescriptors[index].ParameterType);
                            paramDescriptors[index].ParameterValue = value;
                            continue;
                        }
                        catch
                        {

                        }
                    }
                }

                if (DefaultParamterValue.TryGetDefaultValue(paramDescriptors[index].ParameterInfo, out var paramValue))
                {
                    // 注：默认值可能为Null
                    paramDescriptors[index].ParameterValue = paramValue;
                    continue;
                }

                paramValue = ModelBinders.Binders.GetBinder(paramDescriptors[index].ParameterType)
                    .BindModel(controller.ControllerContext, bindingContext);
                if (paramValue == null)
                {
                    paramValue = ModelBinders.Binders[typeof(IServiceProvider)].BindModel(controller.ControllerContext, bindingContext);
                }

                if (paramValue == null)
                {
                    // 默认值为空的情况已经被选出来了，因此如果值为空则说明元数据没有绑定成功，需要退出循环
                    break;
                }

                paramDescriptors[index].ParameterValue = paramValue;
            }

            if (index < paramDescriptors.Length)
            {
                return false;
            }

            //由于paramDescriptors不负责正确的排序

            return true;
        }
    }
}
