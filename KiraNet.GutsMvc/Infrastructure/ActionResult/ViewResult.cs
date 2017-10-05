using KiraNet.GutsMVC.Implement;
using KiraNet.GutsMVC.View;
using System;
using System.Threading.Tasks;

namespace KiraNet.GutsMVC
{
    public class ViewResult : IActionResult
    {
        public object Model { get; set; }
        //public TempDataDictionary TempData { get; set; }
        //public ViewDataDictionary ViewData { get; set; }
        //public dynamic ViewBag { get; set; }
        public IView View { get; set; }
        public string FolderName { get; set; }
        public string ViewName { get; set; }

        private IView FindView(ControllerContext controllerContext)
        {
            return View ?? ViewEngines.Engines.FindView(controllerContext, FolderName, ViewName);
        }
        public void ExecuteResult(ControllerContext context)
        {
            if (String.IsNullOrWhiteSpace(ViewName))
            {
                ViewName = context.RouteEntity.Action;
            }

            if (View == null)
            {
                View = FindView(context);
            }

            ViewContext viewContext = new ViewContext(context, Model);
            View?.Render(viewContext);
            //if (View != null)
            //{
            //    ViewEngines.Engines.ReleaseView(context, View);
            //}
        }

        public Task ExecuteResultAsync(ControllerContext context)
        {
            ExecuteResult(context);
            return Task.CompletedTask;
        }
    }
}
