using KiraNet.GutsMvc.Implement;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// GutsMvc的核心处理类
    /// </summary>
    internal sealed class MvcHandler : IMiddlewareHandle
    {
        public void MiddlewareExecute(HttpContext context)
        {
            var route = context.RouteEntity;
            if (route == null)
            {
                context.IsCancel = true;
                return;
            }

            var controllerContext = new ControllerContext(context);
            IControllerBulider controllerBuilder = new ControllerBuilder(controllerContext);
            var controller = controllerBuilder.ControllerBuild();

            ExecuteController(controller);

            controllerBuilder.ControllerRelease();
        }

        private void ExecuteController(Controller controller)
        {
            if (controller == null)
            {
                throw new System.ArgumentNullException(nameof(controller));
            }

            //controller.ValueProvider = ValueProviderFactories.Factories.GetValueProvider(controller.ControllerContext);
            //controller.ControllerContext.ActionDescriptor = controller.ControllerContext.ControllerDescriptor.BindingAction(controller);
            //if(controller.ControllerContext.ActionDescriptor == null)
            //{
            //    throw new MissingMethodException($"无法找到指定的Action");
            //}

            controller.Execute();
            // TODO: View的选择
            // TODO: View的渲染
        }
    }
}
