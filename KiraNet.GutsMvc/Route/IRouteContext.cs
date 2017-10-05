namespace KiraNet.GutsMVC.Route
{
    public interface IRouteContext
    {
        HttpContext HttpContext { get; }
        IRoute Route { get; set; }
    }
}
