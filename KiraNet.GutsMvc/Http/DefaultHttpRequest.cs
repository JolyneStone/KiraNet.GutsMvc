using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using KiraNet.GutsMvc.Route;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 默认HTTP请求对象
    /// </summary>
    public class DefaultHttpRequest : HttpRequest
    {
        private HttpContext _context;
        //private FeatureReferences<FeatureInterfaces> _feature;
        //private IFeatureCollection _features;
        private IFormFeature _formFeature;
        public IHttpRequestFeature RequestFeature { get; }
        public DefaultHttpRequest(HttpContext context)
        {
            _context = context;
            //_features = context.HttpContextFeatures;
            this.RequestFeature = context.HttpContextFeatures.Get<IHttpRequestFeature>();
            _formFeature = new FormFeature(this);
        }

        public override HttpContext HttpContext => _context ?? throw new NullReferenceException(nameof(_context));
        public override RouteEntity RouteEntity { get; internal set; }
        //public override RouteData RouteData { get; internal set; }

        public override IFormCollection Form
        {
            get => _formFeature.ReadForm();
            internal set => _formFeature.Form = value;
        }
        public override Uri Url => RequestFeature.Url;

        public override string HttpMethod => RequestFeature.HttpMethod;
        public override string PathBase => RequestFeature.PathBase;

        public override string[] AcceptTypes => RequestFeature.AcceptTypes;

        public override int ClientCertificateError => RequestFeature.ClientCertificateError;

        public override string[] UserLanguager => RequestFeature.UserLanguager;

        public override CookieCollection Cookies => RequestFeature.Cookies;
        public override Encoding ContentEncoding => RequestFeature.ContentEncoding;

        public override long ContentLength64 => RequestFeature.ContentLength64;

        public override string ContentType => RequestFeature.ContentType;

        public override bool HasEntityBody => RequestFeature.HasEntityBody;

        public override NameValueCollection Headers => RequestFeature.Headers;

        public override bool IsAuthenticated => RequestFeature.IsAuthenticated;

        public override bool IsLocal => RequestFeature.IsLocal;

        public override bool IsSecureConnection => RequestFeature.IsSecureConnection;

        public override bool IsWebSocketRequest => RequestFeature.IsWebSocketRequest;

        public override bool KeepAlive => RequestFeature.KeepAlive;

        public override IPEndPoint LocalEndPoint => RequestFeature.LocalEndPoint;

        public override NameValueCollection QueryString => RequestFeature.QueryString;

        public override Version ProtocolVersion => RequestFeature.ProtocolVersion;

        public override string RawUrl => RequestFeature.RawUrl;

        public override IPEndPoint RemoteEndPoint => RequestFeature.RemoteEndPoint;

        public override Stream RequestStream { get => RequestFeature.RequestStream; set => RequestFeature.RequestStream = value; }

        public override Guid RequestTraceIdentifier => RequestFeature.RequestTraceIdentifier;

        public override string ServiceName => RequestFeature.ServiceName;

        public override TransportContext TransportContext => RequestFeature.TransportContext;

        public override Uri UrlReferrer => RequestFeature.UrlReferrer;

        public override string UserAgent => RequestFeature.UserAgent;

        public override string UserHostAddress => RequestFeature.UserHostAddress;

        public override string UserHostName => RequestFeature.UserHostName;

        public override X509Certificate2 GetClientCerificate() => RequestFeature.GetClientCerificate();

        public override async Task<X509Certificate2> GetClientCertificateAsync() => await RequestFeature.GetClientCertificateAsync();

        public override IAsyncResult BeginGetClientCertificate(AsyncCallback requestCallback, object state) => RequestFeature.BeginGetClientCertificate(requestCallback, state);

        public override X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult) => RequestFeature.EndGetClientCertificate(asyncResult);

        //struct FeatureInterfaces
        //{
        //    public IHttpRequestFeature Request;
        //    //public IQueryFeature Query;
        //    public IFormFeature Form;
        //    //public IRequestCookiesFeature Cookies;
        //}
    }
}
