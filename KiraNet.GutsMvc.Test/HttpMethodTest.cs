using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace KiraNet.GutsMvc.Test
{
    public class HttpMethodTest
    {
        [Fact]
        [HttpGet]
        [HttpPost]
        public void EnumTest()
        {
            var method = HttpMethod.DELETE | HttpMethod.GET | HttpMethod.POST;

            Assert.True((method & HttpMethod.POST) != 0);
            Assert.False((method & HttpMethod.HEAD) != 0);

            method = method & ~HttpMethod.POST;

            Assert.False((method & HttpMethod.POST) != 0);

        }

        [Fact]
        public void MethodTest()
        {
            var attribute = typeof(HttpMethodTest).GetMethod("EnumTest").GetCustomAttributes<HttpMethodAttribute>().ToArray();
            Assert.True((attribute[0].HttpMethod & HttpMethod.GET) != 0);


            var x = Enum.TryParse<HttpMethod>("get".ToUpper(), out var result);
            Assert.True(result == HttpMethod.GET);
        }
    }
}
