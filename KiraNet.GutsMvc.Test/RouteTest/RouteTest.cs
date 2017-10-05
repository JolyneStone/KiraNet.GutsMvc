
using KiraNet.GutsMVC.Route;
using System;
using Xunit;

namespace KiraNet.GutsMVC.Test.RouteTest
{
    public class RouteTest
    {
        [Fact]
        public void TestController()
        {
            string url = "/account/index/id".TrimStart('/');
            string map = "123{@}/{@}/{@}".TrimStart('/');

            var maps = map.Split('/');
            var urls = url.Split('/');

            var s = "123{@}34".Split("{@}");

            //Assert.True(maps.Length == 3);
            //Assert.True(urls.Length == 3);

            var result = String.Empty;
            for (int i = 0; i < maps.Length && i < urls.Length; i++)
            {
                var index = maps[i].IndexOf("{@}");
                Assert.True(maps[i].Substring(0,index) == "123");
                if (maps[i] == "{@}")
                {
                    result = urls[i];
                    Assert.Equal("account", result);
                    break;
                }

                if (maps[i] != urls[i])
                    break;
            }
        }

        [Fact]
        public void TestRouteMap()
        {
            var routeMap = new RouteMap("default", "/{action}123/{controller=account}/{name?}");
            Assert.Equal(routeMap.DefaultController, "account");
            Assert.Equal(routeMap.DefaultAction, "index");
            Assert.Equal(routeMap.DefaultParameter, "name");
            Assert.Equal(routeMap.Template, "/{@}123/{@}/{@}");
            //var list = routeMap.RouteList;
            //Assert.Equal(list[1], "controller");
            //Assert.Equal(list[0], "action");
            //Assert.Equal(list[2], "parameter");
        }
    }
}
