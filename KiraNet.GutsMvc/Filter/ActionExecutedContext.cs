using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.Filter
{
    public class ActionExecutedContext : ControllerContext
    {
        public ActionExecutedContext()
        {
        }

        public ActionExecutedContext(ControllerContext controllerContext, Exception ex)
        {
            this.ActionDescriptor = controllerContext.ActionDescriptor;
            this.Controller = controllerContext.Controller;
            this.ControllerInfo = controllerContext.ControllerInfo;
            this.HttpContext = controllerContext.HttpContext;
            this.ParameterDescriptors = controllerContext.ParameterDescriptors;
            this.RouteEntity = controllerContext.RouteEntity;
            this.ModelState = controllerContext.ModelState;

            Exception = ex;
        }

        public Exception Exception { get; set; }
        public IActionResult Result { get; set; }

    }
}