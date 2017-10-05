namespace KiraNet.GutsMVC.Route
{
    // 封装对路由的处理
    public class Route : IRoute
    {
        public Route() { }
        public Route(IRouteMatch routeMatch) => RouteMatch = routeMatch;
        public IRouteMatch RouteMatch { get; internal set; } = new RouteMatch();

        public RouteEntity GetRouteEntity(RouteConfiguration routeConfiguration, string url)
        {
            foreach(var map in routeConfiguration.GetRouteMaps())
            {
                if(RouteMatch.Match(map, url, out var routeEntity))
                {
                    return routeEntity;
                }
            }

            return null;
        }
    }
}
