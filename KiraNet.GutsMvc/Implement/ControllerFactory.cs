using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace KiraNet.GutsMVC.Implement
{
    public class ControllerFactory : IControllerFactory
    {
        /// <summary>
        /// 如果需要自定义IControllerFactory则设置该值即可
        /// </summary>
        public static IControllerFactory DefaultControllerFactory { get; set; } = new ControllerFactory();
        public Controller CreateController(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var serviceProvider = context.HttpContext.Service;
            var constructors = context.ControllerInfo.GetConstructors()
                .Where(constructor => constructor.IsPublic)
                .OrderBy(constructor => constructor.GetParameters().Length);

            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                if (parameters == null || parameters.Length == 0)
                {
                    return (Controller)constructor.Invoke(null);
                }

                var paramValues = new object[parameters.Length];
                var index = 0;
                for (; index < parameters.Length; index++)
                {
                    var value = serviceProvider.GetRequiredService(parameters[index].ParameterType);
                    if (value == null)
                    {
                        break;
                    }

                    paramValues[index] = value;
                }

                if (index < parameters.Length)
                    continue;

                return (Controller)constructor.Invoke(paramValues);
            }

            throw new InvalidOperationException($"无法构造出控制器：{context.ControllerInfo.Name}");
        }

        public void DisposeController(ControllerContext context, Controller controller)
        {
            controller?.Dispose();
        }
    }


    //public class ControllerFactory : IControllerFactory
    //{
    //    private readonly IControllerActivator _controllerActivator;
    //    private readonly IControllerPropertyActivator[] _propertyActivators;

    //    public ControllerFactory(
    //        IControllerActivator controllerActivator,
    //        IEnumerable<IControllerPropertyActivator> propertyActivators)
    //    {
    //        if (propertyActivators == null)
    //        {
    //            throw new ArgumentNullException(nameof(propertyActivators));
    //        }

    //        _controllerActivator = controllerActivator ?? throw new ArgumentNullException(nameof(controllerActivator));
    //        _propertyActivators = propertyActivators.ToArray();
    //    }

    //    protected IControllerActivator ControllerActivator => _controllerActivator;

    //    public virtual Controller CreateController(ControllerContext context)
    //    {
    //        if (context == null)
    //        {
    //            throw new ArgumentNullException(nameof(context));
    //        }

    //        if (context.ActionDescriptor == null)
    //        {
    //            throw new ArgumentException($"CreateController()的参数context其ActionDesctiptor属性不能为空。");
    //        }

    //        var controller = _controllerActivator.Create(context);
    //        foreach (var propertyActivator in _propertyActivators)
    //        {
    //            propertyActivator.Activate(context, controller);
    //        }

    //        return controller;
    //    }

    //    public virtual void DisposeController(ControllerContext context, Controller controller)
    //    {
    //        if (context == null)
    //        {
    //            throw new ArgumentNullException(nameof(context));
    //        }

    //        if (controller == null)
    //        {
    //            throw new ArgumentNullException(nameof(controller));
    //        }

    //        _controllerActivator.Release(context, controller);
    //    }
    //}
}
