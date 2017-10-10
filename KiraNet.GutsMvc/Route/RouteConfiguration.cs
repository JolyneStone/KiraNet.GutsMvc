using KiraNet.GutsMvc.Helper;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.Route
{
    public class RouteConfiguration
    {
        private static RouteConfiguration _routeConfig;
        private static KiraSpinLock _lock = new KiraSpinLock();

        // 单例模式
        public static RouteConfiguration RouteConfig
        {
            get
            {
                if (_routeConfig == null)
                {
                    _lock.Enter();
                    if (_routeConfig == null)
                    {
                        _routeConfig = new RouteConfiguration();
                    }
                    _lock.Exit();
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
