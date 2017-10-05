namespace KiraNet.GutsMVC.Route
{
    public interface IRoute
    {
        RouteEntity GetRouteEntity(RouteConfiguration routeConfiguration, string url);
        //IRouteMatch RouteMatch { get; }
    }
}
