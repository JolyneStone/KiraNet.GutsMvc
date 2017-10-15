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

            ExecuteMvc(context);
        }



        private void ExecuteMvc(HttpContext context)
        {

            var controllerContext = new ControllerContext(context);
            IControllerBulider controllerBuilder = new ControllerBuilder(controllerContext);
            var controller = controllerBuilder.ControllerBuild();

            controllerBuilder.ControllerRelease();

            controller.Execute();
        }
    }
}
