using System;

namespace KiraNet.GutsMvc
{
    [AttributeUsage(
        AttributeTargets.Method,
        AllowMultiple = false,
        Inherited = true)]
    public class RouteActionAttribute : Attribute
    {
        public string RouteAction { get; }
        public RouteActionAttribute(string routeAction)
        {
            if (String.IsNullOrWhiteSpace(routeAction))
            {
                throw new ArgumentNullException(routeAction);
            }

            RouteAction = routeAction;
        }
    }
}
