using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.View
{
    public class ViewContext/* : ControllerContext*/
    {
        private ControllerContext _controllerContext;
        public ViewContext(ControllerContext controllerContext, object model)
        {

            //ControllerInfo = controllerContext.ControllerInfo;
            //ControllerDescriptor = controllerContext.ControllerDescriptor;
            //ActionDescriptor = controllerContext.ActionDescriptor;
            //Controller = controllerContext.Controller;
            //ParameterDescriptors = controllerContext.ParameterDescriptors;
            //TempData = controllerContext.Controller.TempData;
            //ViewBag = controllerContext.Controller.ViewBag;
            //ViewData = controllerContext.Controller.ViewData;
            //RouteEntity = controllerContext.Controller.RouteEntity;
            //HttpContext = controllerContext.HttpContext;
            //FolderName = controllerContext.ControllerDescriptor.ControllerName;
            //ViewName = controllerContext.ActionDescriptor.ActionName;
            _controllerContext = controllerContext ?? throw new System.ArgumentNullException(nameof(controllerContext));
            Model = model;
            ModelType = controllerContext.ModelType;
        }


        //public string FolderName { get; }
        //public string ViewName { get; set; }
        public object Model { get; }
        public Type ModelType { get; }
        public HttpContext HttpContext => _controllerContext.HttpContext;
        public ViewDataDictionary ViewData => _controllerContext.Controller.ViewData;
        public TempDataDictionary TempData => _controllerContext.Controller.TempData;
        public DynamicViewBag ViewBag => _controllerContext.Controller.ViewBag;
        //public RouteEntity RouteEntity { get; }
        //public IView View { get; set; }
        ////public TextWriter Writer { get; set; }
        //public TypeInfo ControllerInfo { get; }
        //public ControllerDescriptor ControllerDescriptor { get; }
        //public ActionDescriptor ActionDescriptor { get; }
        //public Controller Controller { get; }
        //public ParameterDescriptor[] ParameterDescriptors { get; }
    }
}