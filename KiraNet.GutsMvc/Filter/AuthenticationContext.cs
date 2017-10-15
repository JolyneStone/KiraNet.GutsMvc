using KiraNet.GutsMvc.Implement;
using System.Security.Principal;

namespace KiraNet.GutsMvc.Filter
{
    public class AuthenticationContext : ControllerContext
    {
        public AuthenticationContext()
        {
        }

        public AuthenticationContext(ControllerContext controllerContext, IPrincipal principal)
        {
            this.ActionDescriptor = controllerContext.ActionDescriptor;
            this.Controller = controllerContext.Controller;
            this.ControllerInfo = controllerContext.ControllerInfo;
            this.HttpContext = controllerContext.HttpContext;
            this.ParameterDescriptors = controllerContext.ParameterDescriptors;
            this.RouteEntity = controllerContext.RouteEntity;
            this.ModelState = controllerContext.ModelState;

            Principal = principal;
        }

        public IPrincipal Principal { get; set; }

        public IActionResult Result { get; set; }
    }
}