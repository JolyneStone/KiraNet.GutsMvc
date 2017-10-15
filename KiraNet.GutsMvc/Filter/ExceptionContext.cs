using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.Filter
{
    public class ExceptionContext:ControllerContext
    {
        public ExceptionContext()
        {
        }

        public ExceptionContext(ControllerContext controllerContext)
        {
            this.ActionDescriptor = controllerContext.ActionDescriptor;
            this.Controller = controllerContext.Controller;
            this.ControllerInfo = controllerContext.ControllerInfo;
            this.HttpContext = controllerContext.HttpContext;
            this.ParameterDescriptors = controllerContext.ParameterDescriptors;
            this.RouteEntity = controllerContext.RouteEntity;
            this.ModelState = controllerContext.ModelState;
        }

        public Exception Exception { get; set; }
        public IActionResult Result { get; set; }
    }
}