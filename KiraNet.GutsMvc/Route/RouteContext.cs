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
        }
    }
}
