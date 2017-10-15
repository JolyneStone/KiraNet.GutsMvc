using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.Implement
{
    // 我们先用表达式构造出委托Fun<ControllerType, object[], ReturnType>
    // 其中ControllerType、object[]、ReturnType分别为控制器类型、Action方法参数数组和返回值类型
    // 然后根据返回值类型，选择同步或异步调用Action方法

    /// <summary>
    /// 用表达式的方式去调用Action方法
    /// </summary>
    public class ExpressionActionInvoker : ActionInvoker
    {
        // 用于缓存Invoke方法的MethodInfo
        private static MethodInfo _invokeMethod;
        protected override void InvokeAction(ControllerContext controllerContext, object[] paramValues)
        {
            if (controllerContext == null)
            {
                throw new System.ArgumentNullException(nameof(controllerContext));
            }

            var returnType = controllerContext.ActionDescriptor.Action.ReturnType;
            if (_invokeMethod == null)
            {
                _invokeMethod = typeof(ExpressionActionInvoker).GetMethod(
                    "Invoke",
                    BindingFlags.IgnoreCase |
                    BindingFlags.Instance |
                    BindingFlags.NonPublic);
            }

            Expression callExpr = Expression.Call(
                Expression.Constant(this),
                _invokeMethod.MakeGenericMethod(returnType),
                Expression.Constant(controllerContext),
                Expression.Constant(paramValues, typeof(object[])),
                Expression.Constant("0")
                );

            Expression.Lambda<Action>(callExpr).Compile()();
        }

        protected override void InvokeActionAsync(ControllerContext controllerContext, object[] paramValues)
        {
            if (controllerContext == null)
            {
                throw new System.ArgumentNullException(nameof(controllerContext));
            }

            var returnType = controllerContext.ActionDescriptor.Action.ReturnType.GetGenericArguments()[0];
            if (_invokeMethod == null)
            {
                _invokeMethod = typeof(ExpressionActionInvoker).GetMethod(
                    "Invoke",
                    BindingFlags.IgnoreCase |
                    BindingFlags.Instance |
                    BindingFlags.NonPublic);
            }

            Expression callExpr = Expression.Call(
                Expression.Constant(this),
                _invokeMethod.MakeGenericMethod(returnType),
                Expression.Constant(controllerContext, typeof(ControllerContext)),
                Expression.Constant(paramValues, typeof(object[])),
                Expression.Constant("1", typeof(string))
                );

            Expression.Lambda<Action>(callExpr).Compile()();
        }

        /// <summary>
        /// 实际调用Action
        /// </summary>
        /// <typeparam name="T">表示IActionResult的类型</typeparam>
        /// <param name="controllerContext"></param>
        /// <param name="isActionAsync">"0"代表同步调用，否则代表异步调用</param>
        private async void Invoke<T>(ControllerContext controllerContext, object[] paramValues, string isActionAsync)
            where T : IActionResult
        {
            IActionResult actionResult;
            //var parameterValues = GetParameterValues(controllerContext.ParameterDescriptors);

            if (isActionAsync == "0")
            {
                // 同步调用
                var actionFunc = ConstructorFunc<T>(controllerContext);
                actionResult = actionFunc(controllerContext.Controller, paramValues);
                actionResult.ExecuteResult(controllerContext);
            }
            else
            {
                // 异步调用
                var actionFunc = ConstructorFunc<Task<T>>(controllerContext);
                actionResult = await (actionFunc(controllerContext.Controller, paramValues) as Task<T>);
                await actionResult.ExecuteResultAsync(controllerContext);
            }
        }

        //private static object[] GetParameterValues(ParameterDescriptor[] parameterDescriptors)
        //{
        //    if (parameterDescriptors == null || parameterDescriptors.Length == 0)
        //    {
        //        return null;
        //    }

        //    object[] parameterValues = new object[parameterDescriptors.Length];
        //    for (int i = 0; i < parameterValues.Length; i++)
        //    {
        //        parameterValues[i] = parameterDescriptors[i].ParameterValue;
        //    }

        //    return parameterValues;
        //}

        private Func<Controller, object[], T> ConstructorFunc<T>(ControllerContext controllerContext)
        {
            Type controllerType = controllerContext.ControllerInfo.AsType();

            // 由于我们所拥有的控制器实例controllerContext.Controller是Controller类型
            // 因此只能用Controller作为传入的参数
            // 之后再用(ControllerType)controller的方式进行显式转换
            // 这样才能调用controller相应的Action方法
            ParameterExpression target = Expression.Parameter(typeof(Controller), "controller");
            ParameterExpression arguments = Expression.Parameter(typeof(object[]), "arguments");
            var method = controllerContext.ActionDescriptor.Action;
            var parameterDescriptors = controllerContext.ParameterDescriptors;
            Expression[] parameters = new Expression[parameterDescriptors.Length];

            // 构造Action方法的参数表达式
            for (int i = 0; i < parameterDescriptors.Length; i++)
            {
                BinaryExpression getElementByIndex = Expression.ArrayIndex(arguments, Expression.Constant(i));
                UnaryExpression convertToParameterType = Expression.Convert(getElementByIndex, parameterDescriptors[i].ParameterType);
                parameters[i] = convertToParameterType;
            }

            // 显式转换表达式
            UnaryExpression instanceCast = Expression.Convert(target, controllerType);
            MethodCallExpression methodCall = Expression.Call(instanceCast, method, parameters);

            Type returnType = method.ReturnType;
            UnaryExpression convertToObjectType = Expression.Convert(methodCall, returnType);
            var actionFunc = Expression.Lambda<Func<Controller, object[], T>>(convertToObjectType, target, arguments).Compile();

            return actionFunc;
        }
    }
}