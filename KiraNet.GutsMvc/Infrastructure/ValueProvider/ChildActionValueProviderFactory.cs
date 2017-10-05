//using KiraNet.GutsMVC.Implement;
//using System;

//namespace KiraNet.GutsMVC.Infrastructure
//{
//    public sealed class ChildActionValueProviderFactory : IValueProviderFactory
//    {
//        public IValueProvider CreateValueProvider(ControllerContext controllerContext)
//        {
//            if(controllerContext == null)
//            {
//                throw new ArgumentNullException(nameof(controllerContext));
//            }

//            try
//            {
//                return new ChildActionValueProvider(controllerContext.RouteData);
//            }
//            catch
//            {
//                return null;
//            }
//        }
//    }
//}
