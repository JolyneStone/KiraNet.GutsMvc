using KiraNet.GutsMvc.ModelValid;
using KiraNet.GutsMvc.Route;
using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace KiraNet.GutsMvc.Implement
{
    public class ControllerContext : ActionContext
    {
        //    private IList<IValueProviderFactory> _valueProviderFactories;
        public TypeInfo ControllerInfo { get; set; }
        //public MethodInfo MethodInfo { get; set; }
        //public RouteData RouteData { get; internal set; }
        internal ControllerDescriptor ControllerDescriptor { get; set; }
        internal ActionDescriptor ActionDescriptor { get; set; }
        internal ParameterDescriptor[] ParameterDescriptors { get; set; }
        internal Type ModelType { get; set; }
        public Controller Controller { get; set; }
        public IModelState ModelState { get; set; }
        public ControllerContext() { }
        public ControllerContext(ActionContext context)
            : base(context)
        {
        }

        public ControllerContext(HttpContext httpContext, RouteEntity routeEntity) : base(httpContext, routeEntity)
        {
            //RouteData = httpContext.RouteData;
            ModelState = httpContext.Service.GetService<IModelState>();
        }

        public ControllerContext(HttpContext httpContext) : this(httpContext, httpContext.RouteEntity)
        {
        }

        //public virtual IList<IValueProviderFactory> ValueProviderFactories
        //{
        //    get
        //    {
        //        if (_valueProviderFactories == null)
        //        {
        //            _valueProviderFactories = new List<IValueProviderFactory>();
        //        }

        //        return _valueProviderFactories;
        //    }
        //    set => _valueProviderFactories = value ?? throw new ArgumentNullException(nameof(value));
        //}

        //internal ActionDescriptor SelectAction()
        //{
        //    if(ControllerDescriptor!=null)
        //    {
        //        new InvalidOperationException($"在{nameof(ControllerDescriptor)}属性被赋值前被使用");
        //    }

        //    foreach(var actionDescriptor in ControllerDescriptor.FindActions(this))
        //    {

        //    }
        //}
    }
}