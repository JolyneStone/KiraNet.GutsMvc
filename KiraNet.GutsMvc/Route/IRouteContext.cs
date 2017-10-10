namespace KiraNet.GutsMvc.Route
{
    public interface IRouteContext
    {
        HttpContext HttpContext { get; }
        IRoute Route { get; set; }
    }
}
