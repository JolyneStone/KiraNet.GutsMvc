using KiraNet.GutsMvc.Implement;
using KiraNet.GutsMvc.View;
using System;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
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

        private IView GetView(ControllerContext controllerContext)
        {
            return View ?? ViewEngine.Current.CreateView(controllerContext, FolderName, ViewName);
        }
        public void ExecuteResult(ControllerContext context)
        {
            if (String.IsNullOrWhiteSpace(ViewName))
            {
                ViewName = context.RouteEntity.Action;
            }

            if (View == null)
            {
                View = GetView(context);
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
