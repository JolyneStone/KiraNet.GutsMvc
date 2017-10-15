using KiraNet.GutsMvc.Implement;

namespace KiraNet.GutsMvc.Filter
{
    public class AuthorizationContext:ControllerContext
    {
        public AuthorizationContext()
        {
        }

        public AuthorizationContext(ControllerContext controllerContext)
        {
            this.ActionDescriptor = controllerContext.ActionDescriptor;
            this.Controller = controllerContext.Controller;
            this.ControllerInfo = controllerContext.ControllerInfo;
            this.HttpContext = controllerContext.HttpContext;
            this.ParameterDescriptors = controllerContext.ParameterDescriptors;
            this.RouteEntity = controllerContext.RouteEntity;
            this.ModelState = controllerContext.ModelState;
        }

        public IActionResult Result { get; set; }
    }
}