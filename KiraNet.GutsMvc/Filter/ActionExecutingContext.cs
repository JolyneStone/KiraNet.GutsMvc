using KiraNet.GutsMvc.Implement;

namespace KiraNet.GutsMvc.Filter
{
    public class ActionExecutingContext : ControllerContext
    {
        public ActionExecutingContext()
        {

        }

        public ActionExecutingContext(ControllerContext controllerContext, object[] paramValues)
        {
            this.ActionDescriptor = controllerContext.ActionDescriptor;
            this.Controller = controllerContext.Controller;
            this.ControllerInfo = controllerContext.ControllerInfo;
            this.HttpContext = controllerContext.HttpContext;
            this.ParameterDescriptors = controllerContext.ParameterDescriptors;
            this.RouteEntity = controllerContext.RouteEntity;
            this.ModelState = controllerContext.ModelState;

            ActionParameters = paramValues;
        }

        public object[] ActionParameters { get; set; }

        public IActionResult Result { get; set; }
    }
}