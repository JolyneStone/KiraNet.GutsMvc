using System.Collections.Generic;

namespace KiraNet.GutsMVC.Route
{
    public class RouteConfiguration
    {
        private static RouteConfiguration _routeConfig;
        private static object _sync = new object();

        // 单例模式
        public static RouteConfiguration RouteConfig
        {
            get
            {
                if(_routeConfig == null)
                {
                    lock(_sync)
                    {
                        if(_routeConfig==null)
                        {
                            _routeConfig = new RouteConfiguration();
                        }
                    }
                }

                return _routeConfig;
            }
        }

        private RouteConfiguration()
        {
        }

        public static int Port { get; set; } = 17758;

        private IList<RouteMap> _maps = new List<RouteMap>();

        public void AddRouteMap(string name, string template, int port = 17758)
        {
            Port = port;
            _maps.Add(new RouteMap(name, template));
        }

        public void AddRouteMap(string name, string controller, string action, string parameter = "id", int port = 17758)
        {
            Port = port;
            _maps.Add(new RouteMap(name, controller, action, parameter));
        }

        public IEnumerable<RouteMap> GetRouteMaps()
            => _maps;
    }
}
