using KiraNet.GutsMvc.Implement;
using KiraNet.GutsMvc.View;
using System;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public class ViewResult : IActionResult
    {
        public object Model { get; set; }
        public Type ModelType { get; set; }
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

            if(ModelType!=null)
            {
                context.ModelType = ModelType;
            }

            ViewContext viewContext = new ViewContext(context, Model);
            View?.Render(viewContext);
            //if (View != null)
            //{
            //    ViewEngines.Engines.ReleaseView(context, View);
            //}
        }

        public async Task ExecuteResultAsync(ControllerContext context)
        {
            if (String.IsNullOrWhiteSpace(ViewName))
            {
                ViewName = context.RouteEntity.Action;
            }

            if (View == null)
            {
                View = GetView(context);
            }

            if (ModelType != null)
            {
                context.ModelType = ModelType;
            }

            ViewContext viewContext = new ViewContext(context, Model);
            await View?.RenderAsync(viewContext);
        }
    }
}
