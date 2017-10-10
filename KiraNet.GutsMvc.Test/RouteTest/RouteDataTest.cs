using KiraNet.GutsMvc.Route;
using System;
using Xunit;

namespace KiraNet.GutsMvc.Test.RouteTest
{
    public class RouteDataTest
    {
        public class TestClass
        {
            public int Id { get; set; }
            public int Td { private get; set; }
            public object Obj1 { get; set; }
            private object Obj2 { get; set; }
        }
        [Fact]
        public void Test()
        {
            var test = new TestClass { Id = 1, Td = 2, Obj1 = "123" };
            var routeData = new RouteData(test);
 
            Assert.True(Int32.Equals(routeData["Id"], 1));
            Assert.True(Int32.Equals(routeData["Td"], 2));
            Assert.True(String.Equals(routeData["Obj1"], "123"));
            Assert.True(!routeData.ContainsKey("Obj2"));
        }
    }
}
