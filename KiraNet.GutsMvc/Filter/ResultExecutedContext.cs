using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.Filter
{
    public class ResultExecutedContext:ControllerContext
    {
        public ResultExecutedContext()
        {
        }

        public ResultExecutedContext(ControllerContext controllerContext, Exception ex)
        { 
            this.ActionDescriptor = controllerContext.ActionDescriptor;
            this.Controller = controllerContext.Controller;
            this.ControllerInfo = controllerContext.ControllerInfo;
            this.HttpContext = controllerContext.HttpContext;
            this.ParameterDescriptors = controllerContext.ParameterDescriptors;
            this.RouteEntity = controllerContext.RouteEntity;
            this.ModelState = controllerContext.ModelState;

            Exception = ex;
            ExceptionHandled = ex == null;
        }

        public Exception Exception { get; set; }
        public bool ExceptionHandled { get; set; }
        public bool Cancel { get; set; }
        public IActionResult Result { get; set; }
    }
}