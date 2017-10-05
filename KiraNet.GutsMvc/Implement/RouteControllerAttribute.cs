using System;

namespace KiraNet.GutsMVC
{
    [AttributeUsage(
        AttributeTargets.Class,
        AllowMultiple = false,
        Inherited = true)]
    public class RouteControllerAttribute : Attribute
    {
        public string RouteController { get; }
        public RouteControllerAttribute(string routeController)
        {
            if (String.IsNullOrWhiteSpace(routeController))
            {
                throw new ArgumentNullException(nameof(routeController));
            }

            RouteController = routeController;
        }
    }
}
