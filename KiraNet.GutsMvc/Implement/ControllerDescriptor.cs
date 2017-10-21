using KiraNet.GutsMvc.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.Implement
{
    /// <summary>
    /// 用于描述Controller
    /// </summary>
    public class ControllerDescriptor : IFilterAttributeProvider
    {
        /// <summary>
        /// 除去"Controller"后缀
        /// </summary>
        public string ControllerName { get; set; }
        public TypeInfo ControllerType { get; set; }
        public IServiceProvider Services { get; set; }

        public IEnumerable<FilterAttribute> GetFilterAttributes()
        {
            if (ControllerType == null)
            {
                return null;
            }

            return ControllerType.GetCustomAttributes<FilterAttribute>();
        }

        internal ActionDescriptor BindingAction(Controller controller)
        {
            if (controller.ControllerContext.ControllerDescriptor != null)
            {
                new InvalidOperationException($"在{nameof(ControllerDescriptor)}属性被赋值前被使用");
            }

            foreach (var actionDescriptor in controller.ControllerContext.ControllerDescriptor.FindActions(controller.ControllerContext))
            {
                if (!actionDescriptor.TryBindingParameter(controller, out var paramDescriptors))
                {
                    controller.ControllerContext.ParameterDescriptors = null;
                    continue;
                }

                controller.ControllerContext.ActionDescriptor = actionDescriptor;
                controller.ControllerContext.ParameterDescriptors = paramDescriptors;
                return actionDescriptor;
            }

            return null;
        }

        internal IEnumerable<ActionDescriptor> FindActions(ControllerContext context)
        {
            if (ControllerType == null)
            {
                yield break;
            }

            var actionMethods = ControllerType.GetMethods(
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.IgnoreCase |
                    BindingFlags.InvokeMethod)
                    .OrderByDescending(x => x.GetParameters().Length)
                    .OrderByDescending(x => x.IsDefined(typeof(HttpMethodAttribute)));
            if (actionMethods == null && !actionMethods.Any())
            {
                yield break;
            }

            foreach (var method in actionMethods)
            {
                // Action方法不支持泛型参数
                if (method.IsGenericMethod)
                {
                    continue;
                }

                // Action方法返回值约束：
                // 1. 返回不准为void
                // 2. IActionResult或其实现类型
                // 3. Task<T>，其中T为IActionResult或其实现类型
                var result = method.ReturnType;
                if (result == typeof(void))
                {
                    continue;
                }
                if (!typeof(IActionResult).IsAssignableFrom(result))
                {
                    if (!result.IsGenericType)
                    {
                        continue;
                    }

                    if ((result.GetGenericTypeDefinition() != typeof(Task<>) ||
                    !typeof(IActionResult).IsAssignableFrom(result.GetGenericArguments()[0])))
                    {
                        continue;
                    }
                }

                var parameters = method.GetParameters();
                if (parameters != null || parameters.Any())
                {
                    // Action方法中参数不能有ref和out修饰符
                    if (parameters.FirstOrDefault(x => x.IsOut || x.ParameterType.IsByRef) != null)
                    {
                        continue;
                    }
                }

                var actionName = context.RouteEntity.Action;
                var paramName = context.RouteEntity.DefaultParameter;
                if (method.IsDefined(typeof(RouteActionAttribute)))
                {
                    var routeActionAttribute = method.GetCustomAttribute<RouteActionAttribute>();
                    if (!String.Equals(routeActionAttribute.RouteAction, actionName, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }
                else if (!String.Equals(method.Name, actionName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (!Enum.TryParse<HttpMethod>(context.HttpContext.Request.HttpMethod.ToUpper(), out var routeMethod))
                {
                    routeMethod = HttpMethod.GET;
                }

                var methods = method.GetCustomAttributes<HttpMethodAttribute>();
                if (methods == null || methods.Count() == 0)
                {
                    if (routeMethod == HttpMethod.GET)
                    {
                        yield return new ActionDescriptor { Action = method, ActionName = method.Name.ToLower(), Services = Services };
                    }
                    else
                    {
                        continue;
                    }
                }

                if (methods.FirstOrDefault(x => x.HttpMethod == routeMethod) != null)
                {
                    yield return new ActionDescriptor { Action = method, ActionName = method.Name.ToLower(), Services = Services };
                }

                continue;
            }
        }
    }
}
