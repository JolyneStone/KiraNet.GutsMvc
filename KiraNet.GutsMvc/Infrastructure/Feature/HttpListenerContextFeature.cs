using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 由于HttpListenerServer采用一个HttpListener对象作为监听器，由它接收的请求将被封装成一个类型为HttpListenerContext的上下文对象。
    /// 我们通过一个HttpListenerContextFeature类型来封装这个HttpListenerContext对象。
    /// HttpApplication所代表的中间件不仅仅利用这个特性获取所有与请求相关的信息，而且针对请求的任何响应也都是利用这个特性来实现的。 
    /// </summary>
    public class HttpListenerContextFeature : IHttpListenerContextFeature
    {
        private readonly HttpListenerContext _context;
        public HttpListenerContextFeature(HttpListenerContext context, HttpListener listener)
        {
            this._context = context;
            RequestFeature = new HttpRequestFeature(context.Request, listener);
            ResponseFeature = new HttpResponseFeature(context.Response, listener);
            WebSocketFeature = new WebSocketFeature(context.AcceptWebSocketAsync);
            //Url = context.Request.Url;
            //ResponseStream = context.Response.OutputStream;
            //PathBase = (from it in listener.Prefixes
            //                 let pathBase = new Uri(it).LocalPath.TrimEnd('/')
            //                 where Url.LocalPath.StartsWith(pathBase, StringComparison.OrdinalIgnoreCase)
            //                 select pathBase).First();
        }

        //public string ContentType
        //{
        //    get { return _context.Response.ContentType; }
        //    set { _context.Response.ContentType = value; }
        //}

        //public Stream ResponseStream { get; }

        //public int StatusCode
        //{
        //    get { return _context.Response.StatusCode; }
        //    set { _context.Response.StatusCode = value; }
        //}

        //public Uri Url { get; }
        //public string PathBase { get; }

        public IHttpRequestFeature RequestFeature { get; }

        public IHttpResponseFeature ResponseFeature { get; }

        public IWebSocketFeature WebSocketFeature { get; set; }
    }
}

