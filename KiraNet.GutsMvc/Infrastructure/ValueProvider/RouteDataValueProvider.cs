//using KiraNet.GutsMvc.Route;
//using System;
//using System.Globalization;

//namespace KiraNet.GutsMvc.Infrastructure
//{
//    public sealed class RouteDataValueProvider : ValueProvider<DictionaryValueProviderResultWrapper<object>>
//    {
//        public RouteDataValueProvider(RouteData routeData):this(routeData, CultureInfo.CurrentCulture)
//        {
//        }

//        public RouteDataValueProvider(RouteData routeData, CultureInfo culture) : base(culture)
//        {
//            if (routeData == null)
//            {
//                throw new ArgumentNullException(nameof(routeData));
//            }

//            foreach (string key in routeData.Keys)
//            {
//                _values[key] = new DictionaryValueProviderResultWrapper<object>(key, routeData, culture);
//            }
//        }
//    }
//}
