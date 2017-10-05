using System;

namespace KiraNet.GutsMVC
{

    [AttributeUsage(
        AttributeTargets.Method,
        AllowMultiple = false,
        Inherited = true)]
    public class HttpMethodAttribute : Attribute
    {
        public HttpMethod HttpMethod { get; }

        public HttpMethodAttribute(HttpMethod method)
        {
            HttpMethod = method;
        }
    }

    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute() : base(HttpMethod.GET)
        {
        }
    }

    public class HttpPostAttribute : HttpMethodAttribute
    {
        public HttpPostAttribute() : base(HttpMethod.POST)
        {
        }
    }

    public class HttpHeadAttribute : HttpMethodAttribute
    {
        public HttpHeadAttribute() : base(HttpMethod.HEAD)
        {
        }
    }

    public class HttpPutAttribute : HttpMethodAttribute
    {
        public HttpPutAttribute() : base(HttpMethod.PUT)
        {
        }
    }

    public class HttpDeleteAttribute : HttpMethodAttribute
    {
        public HttpDeleteAttribute() : base(HttpMethod.DELETE)
        {
        }
    }

    public class HttpOptionsAttribute : HttpMethodAttribute
    {
        public HttpOptionsAttribute() : base(HttpMethod.OPTIONS)
        {
        }
    }

    public class HttpTraceAttribute : HttpMethodAttribute
    {
        public HttpTraceAttribute() : base(HttpMethod.TRACE)
        {
        }
    }

    public class HttpConnectAttribute : HttpMethodAttribute
    {
        public HttpConnectAttribute() : base(HttpMethod.CONNECT)
        {
        }
    }

    [Flags]
    public enum HttpMethod
    {
        GET = 0x1,
        POST = 0x2,
        HEAD = 0x4,
        PUT = 0x8,
        DELETE = 0x10,
        OPTIONS = 0x20,
        TRACE = 0x40,
        CONNECT = 0x80
    }
}
