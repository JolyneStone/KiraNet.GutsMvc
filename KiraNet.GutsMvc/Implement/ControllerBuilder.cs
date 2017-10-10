using System;

namespace KiraNet.GutsMvc.Implement
{
    public class ControllerBuilder : IControllerBulider
    {
        private ControllerContext _controllerContext;
        private IControllerFactoryProvider _controllerFactoryProvider;
        private Controller _controller;
        public ControllerBuilder(ControllerContext controllerContext)
        {
            _controllerContext = controllerContext ?? throw new ArgumentNullException(nameof(controllerContext));
            _controllerFactoryProvider = new ControllerFactoryProvider(new ControllerProvider());
        }

        public Controller ControllerBuild()
        {
            var route = _controllerContext.RouteEntity;
            _controller = _controllerFactoryProvider
                .CreateControllerFactory(new ControllerDescriptor { ControllerName = route.Controller.ToLower() })(_controllerContext);
            _controllerContext.Controller = _controller;
            _controller.ControllerContext = _controllerContext;
            return _controller;
        }

        public void ControllerRelease()
        {
            _controller.Dispose();
            _controller = null;
            _controllerContext = null;
            _controllerFactoryProvider = null;
        }
    }
}
