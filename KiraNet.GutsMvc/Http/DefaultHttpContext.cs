using KiraNet.GutsMvc.Filter;
using KiraNet.GutsMvc.Route;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Principal;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 默认HTTP请求上下文类
    /// 注：虽然我们都是用HttpContext来对HTTP请求进行处理，但实际上真正的上下文是由服务器创建的原始上下文（Server Context），在我们这个WEB框架中就是由服务器HttpListenerServer创建HttpListenerContext。
    /// 但对于不同的服务器来说，其创建的原始上下文各有不同，因此需要由一个统一的标准HttpContext上下文来进行描述和封装
    /// </summary>
    public class DefaultHttpContext : HttpContext
    {
        /// <summary>
        /// 该属性是由服务器创建的用于封装原始HTTP上下文相关特性的对象
        /// </summary>
        public override IFeatureCollection HttpContextFeatures { get; }
        public override IWebSocketFeature WebStocket { get; internal set; }
        public override HttpRequest Request { get; internal set; }
        public override HttpResponse Response { get; internal set; }
        public override RouteContext Route { get; internal set; }
        //public override RouteEntity RouteEntity { get; internal set ; }
        //public override RouteData RouteData { get; internal set; }

        public DefaultHttpContext(IFeatureCollection httpContextFeatures, IServiceProvider service, IServiceProvider serviceScope)
        {
            //this._features = new FeatureReferences<FeatureInterfaces>(httpContextFeatures);
            this.HttpContextFeatures = httpContextFeatures;
            this.WebStocket = httpContextFeatures.Get<IWebSocketFeature>();
            this.Request = new DefaultHttpRequest(this);
            this.Response = new DefaultHttpResponse(this);
            this.ServiceRoot = service; // 这里的ServiceProvider和WebHost不是同一个实例，但同样是根ServiceProvider
            this.Service = Service ?? serviceScope;
            this.BuilderSessionManager();

            Route = new RouteContext(this);
            //RouteData = Request.RouteData;
            RouteEntity = Route.Route.GetRouteEntity(RouteConfiguration.RouteConfig, Request.RawUrl);
        }

        private IPrincipal _user;
        public override IPrincipal User
        {
            get
            {
                if (_user == null)
                {
                    _user = Service.GetService<IClaimSchema>().CreateSchema(this);
                }

                return _user;
            }
            set
            {
                _user = value;
            }
        }

        //private IHttpRequestLifetimeFeature LifetimeFeature 
        //    => _features.Fetch(ref _features.Cache.Lifetime, _newHttpRequestLifetimeFeature);

        //public override CancellationToken RequestAborted
        //{
        //    get { return LifetimeFeature.RequestAborted; }
        //    set { LifetimeFeature.RequestAborted = value; }
        //}

        //public override void Abort()
        //{
        //    LifetimeFeature.Abort();
        //}

        //struct FeatureInterfaces
        //{
        //    //public IItemsFeature Items;
        //    //public IServiceProvidersFeature ServiceProviders;
        //    //public IHttpAuthenticationFeature Authentication;
        //    public IHttpRequestLifetimeFeature Lifetime;
        //    //public ISessionFeature Session;
        //    //public IHttpRequestIdentifierFeature RequestIdentifier;
        //}
    }
}
