using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.View
{
    public abstract class RazorPageViewBase<TModel> : RazorPageViewBase
    {
        public TModel Model { get; protected set; }

        public override async Task ExecuteViewAsync(ViewContext viewContext, Action<StringBuilder> executeChild = null)
        {
            if (viewContext.Model != null)
            {
                Model = (TModel)viewContext.Model;
            }

            await base.ExecuteViewAsync(viewContext, executeChild);
        }
    }

    //internal abstract class RazorPageView : RazorPageViewBase
    //{
    //    public Type ModelType { get; protected set; }

    //    public dynamic Model { get; protected set; }

    //    public override async Task ExecuteViewAsync(HttpContext httpContext, ViewContext viewContext, Func<StreamWriter, Task> executeChild = null)
    //    {
    //        if (ModelType != null)
    //        {
    //            typeof(RazorPageView).GetMethod("ConvertModel",
    //                BindingFlags.IgnoreCase |
    //                BindingFlags.InvokeMethod |
    //                BindingFlags.Instance |
    //                BindingFlags.Public)
    //                .MakeGenericMethod(ModelType)
    //                .Invoke(this, new object[] { viewContext.Model });
    //        }

    //        await base.ExecuteViewAsync(httpContext, viewContext, executeChild);
    //    }

    //    private void ConvertModel<T>(object model)
    //    {
    //        if (model != null)
    //        {
    //            Model = (T)model;
    //        }
    //        else
    //        {
    //            Model = Activator.CreateInstance<T>();
    //        }
    //    }
    //}
}
