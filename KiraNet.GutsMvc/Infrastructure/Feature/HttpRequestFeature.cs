using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMVC
{
    internal class HttpRequestFeature : IHttpRequestFeature
    {
        private HttpListenerRequest _request;
        public HttpRequestFeature(HttpListenerRequest request, HttpListener listener)
        {
            _request = request;
            this.PathBase = "/";
            //this.PathBase = (from it in listener.Prefixes
            //            let pathBase = new Uri(it).LocalPath.TrimEnd('/')
            //            where request.Url.LocalPath.StartsWith(pathBase, StringComparison.OrdinalIgnoreCase)
            //            select pathBase).First();
        }

        //public NameValueCollection Form
        //{
        //    get
        //    {
        //        return new NameValueCollection();
        //    }
        //}

        public Uri Url => _request.Url;

        public string PathBase { get; }

        public NameValueCollection QueryString => _request.QueryString;

        public bool KeepAlive => _request.KeepAlive;

        public bool HasEntityBody => _request.HasEntityBody;

        public Version ProtocolVersion => _request.ProtocolVersion;

        public CookieCollection Cookies => _request.Cookies;

        public TransportContext TransportContext => _request.TransportContext;

        public int ClientCertificateError => _request.ClientCertificateError;

        public string[] UserLanguager => _request.UserLanguages;

        public string UserHostName => _request.UserHostName;

        public string UserHostAddress => _request.UserHostAddress;

        public string UserAgent => _request.UserAgent;

        public Uri UrlReferrer => _request.UrlReferrer;

        public string ServiceName => _request.ServiceName;

        public string RawUrl => _request.RawUrl;

        public IPEndPoint RemoteEndPoint => _request.RemoteEndPoint;

        public IPEndPoint LocalEndPoint => _request.LocalEndPoint;

        public bool IsSecureConnection => _request.IsSecureConnection;

        public bool IsLocal => _request.IsLocal;

        public bool IsAuthenticated => _request.IsAuthenticated;

        public Stream RequestStream => _request.InputStream;

        public string HttpMethod => _request.HttpMethod;

        public NameValueCollection Headers => _request.Headers;

        public string ContentType => _request.ContentType;

        public long ContentLength64 => _request.ContentLength64;

        public Encoding ContentEncoding => _request.ContentEncoding;

        public string[] AcceptTypes => _request.AcceptTypes;

        public Guid RequestTraceIdentifier => _request.RequestTraceIdentifier;

        public bool IsWebSocketRequest => _request.IsWebSocketRequest;

        public Func<AsyncCallback, object, IAsyncResult> BeginGetClientCertificate => _request.BeginGetClientCertificate;

        public Func<IAsyncResult, X509Certificate2> EndGetClientCertificate => _request.EndGetClientCertificate;

        public Func<X509Certificate2> GetClientCerificate => _request.GetClientCertificate;

        public Func<Task<X509Certificate2>> GetClientCertificateAsync => _request.GetClientCertificateAsync;


        Stream IHttpRequestFeature.RequestStream { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}