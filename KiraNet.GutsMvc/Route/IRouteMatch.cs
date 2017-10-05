namespace KiraNet.GutsMVC.Route
{
    public interface IRouteMatch
    {
        bool Match(RouteMap map, string url, out RouteEntity routeEntity);
    }
}