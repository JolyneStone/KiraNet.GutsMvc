using KiraNet.GutsMvc.Route;
using System;
using Xunit;

namespace KiraNet.GutsMvc.Test.RouteTest
{
    public class RouteMatchTest
    {
        [Fact]
        public void Test()
        {
            RouteConfiguration routeConfiguration = RouteConfiguration.RouteConfig;
            routeConfiguration.AddRouteMap("default", "/{controller=home}/{action=index}/{id?}");

            IRoute route = new KiraNet.GutsMvc.Route.Route(new RouteMatch());
            var routeEntity = route.GetRouteEntity(routeConfiguration, "/home/index/1");

            Assert.True(EqualsRouteEntity(routeEntity, new RouteEntity() { Controller="home", Action="index", DefaultParameter="id", ParameterValue = "1" }));
        }

        public bool EqualsRouteEntity(RouteEntity entity1, RouteEntity entity2)
        {
            if (entity1 == null || entity2 == null)
                return false;
            if (!String.Equals(entity1.Controller, entity2.Controller, StringComparison.OrdinalIgnoreCase))
                return false;
            if (!String.Equals(entity1.Action, entity2.Action, StringComparison.OrdinalIgnoreCase))
                return false;
            if (!String.Equals(entity1.DefaultParameter, entity2.DefaultParameter, StringComparison.OrdinalIgnoreCase))
                return false;
            if (!String.Equals(entity1.ParameterValue, entity2.ParameterValue, StringComparison.OrdinalIgnoreCase))
                return false;

            return true;
        }
    }
}
