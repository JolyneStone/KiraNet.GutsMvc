namespace KiraNet.GutsMVC.Route
{
    /// <summary>
    /// 当前所用的路由数据实体
    /// </summary>
    public class RouteEntity
    {
        public string RouteName { get; set; }
        //public int Port { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        //public string HttpMethod { get; set; }
        public string DefaultParameter { get; set; }
        public string ParameterValue { get; set; }
    }
}
