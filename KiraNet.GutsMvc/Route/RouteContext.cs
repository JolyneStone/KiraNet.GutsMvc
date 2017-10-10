using System;
using System.Collections.Generic;
using System.Text;

namespace KiraNet.GutsMvc.Route
{
    public class RouteContext
    {
        public HttpContext HttpContext { get; }
        public IRoute Route { get; }

        public RouteContext(HttpContext context)
        {
            HttpContext = context;
            Route = new Route();
            context.Route = this;
        }
    }
}
