using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 默认服务器
    /// </summary>
    public class HttpListenerServer : IServer
    {
        /// <summary>
        /// 我们使用IServerAddressesFeature表示的特性来提供监听地址
        /// </summary>
        public HttpListener Listener { get; }
        public IFeatureCollection Features { get; }
        public IServiceCollection Services { get; }

        public HttpListenerServer(IServiceCollection services)
        {
            Services = services;
            this.Listener = new HttpListener
            {
                AuthenticationSchemes = AuthenticationSchemes.Anonymous
            };
            this.Features = new FeatureCollection().Set<IServerAddressesFeature>(new ServerAddressesFeature());
        }
        /// <summary>
        /// 开始监听来自网络的HTTP请求。HTTP请求一旦抵达，我们会调用HttpListener的GetContext方法得到表示原始HTTP上下文的HttpListenerContext对象，
        /// 并根据它创建一个类型为HttpListenerContextFeature的特性对象，该对象分别采用类型IHttpRequestFeature和IHttpResponseFeature注册到创建的FeatureCollection对象上
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="application"></param>
        public void Run<TContext>(IHttpApplication<TContext> application) where TContext : Context
        {
            IServerAddressesFeature addressFeatures = this.Features.Get<IServerAddressesFeature>();
            // 添加监视地址
            foreach (string address in addressFeatures.Addresses)
            {
                Listener.Prefixes.Add(address.TrimEnd('/') + "/");
            }

            this.Listener.Start();
            this.Listener.BeginGetContext(GetServerCallback<TContext>, application); // 用异步方式监听HTTP请求
        }

        private void GetServerCallback<TContext>(IAsyncResult ar) where TContext : Context
        {
            var application = ar.AsyncState as IHttpApplication<TContext>;
            HttpListenerContext httpListenerContext = Listener.EndGetContext(ar);

            //if (httpListenerContext.Request.IsWebSocketRequest)
            //{
            //    var socket = httpListenerContext.AcceptWebSocketAsync("gutsmvc").GetAwaiter().GetResult().WebSocket;
            //    while (true)
            //    {
            //        ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
            //        WebSocketReceiveResult result = socket.ReceiveAsync(buffer, CancellationToken.None).GetAwaiter().GetResult();
            //        if (socket.State == WebSocketState.Open)
            //        {
            //            string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
            //            string returnMessage = "You send :" + message + ". at" + DateTime.Now.ToLongTimeString();
            //            buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(returnMessage));
            //            socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None).GetAwaiter().GetResult();
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }

            //}

            Listener.BeginGetContext(GetServerCallback<TContext>, application);

            var t = Task.Factory.StartNew(() =>
            {
                IHttpListenerContextFeature feature = new HttpListenerContextFeature(httpListenerContext, Listener);
                IFeatureCollection contextFeatures = new FeatureCollection(Services)
                    .Set<IHttpRequestFeature>(feature.RequestFeature)
                    .Set<IHttpResponseFeature>(feature.ResponseFeature)
                    .Set<IWebSocketFeature>(feature.WebSocketFeature);

                TContext context = application.CreateContext(contextFeatures);

                //await application.ProcessRequestAsync(context).ConfigureAwait(false).GetAwaiter().GetResult();
                //application.ProcessRequestAsync(context).ConfigureAwait(false).GetAwaiter()
                //.OnCompleted(() => context.Dispose());
                application.ProcessRequestAsync(context).ConfigureAwait(false).GetAwaiter().GetResult();
                context.Dispose();
                //return context;
            });

            //(t.ConfigureAwait(false).GetAwaiter().GetResult()).Dispose();
        }
    }
}