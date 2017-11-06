using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.View
{
    public class ViewContext/* : ControllerContext*/
    {
        public ViewContext(ViewContext viewContext)
        {
            ViewData = viewContext.ViewData;
            TempData = viewContext.TempData;
            ViewBag = viewContext.ViewBag;
            HttpContext = viewContext.HttpContext;
        }
        public ViewContext(ControllerContext controllerContext, object model)
        {
            ViewData = controllerContext.Controller.ViewData;
            TempData = controllerContext.Controller.TempData;
            ViewBag = controllerContext.Controller.ViewBag;
            HttpContext = controllerContext.HttpContext;
            Model = model;
            ModelType = controllerContext.ModelType;
        }

        public object Model { get; }
        public Type ModelType { get; }
        public HttpContext HttpContext { get; }
        public ViewDataDictionary ViewData { get; }
        public TempDataDictionary TempData { get; }
        public DynamicViewBag ViewBag { get; }
    }
}