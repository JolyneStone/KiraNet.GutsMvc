//using KiraNet.GutsMVC.Implement;
//using System;

//namespace KiraNet.GutsMVC.Infrastructure
//{
//    public sealed class RouteDataValueProviderFactory : IValueProviderFactory
//    {
//        public IValueProvider CreateValueProvider(ControllerContext controllerContext)
//        {
//            if (controllerContext == null)
//            {
//                throw new ArgumentNullException(nameof(controllerContext));
//            }

//            try
//            {
//                return new RouteDataValueProvider(controllerContext.HttpContext.Request.RouteData);
//            }
//            catch
//            {
//                return null;
//            }
//        }
//    }
//}
