using System;
using System.IO;
using System.Net;
using System.Text;

namespace KiraNet.GutsMvc
{
    public class HttpResponseFeature : IHttpResponseFeature
    {
        private HttpListenerResponse _response;
        public HttpResponseFeature(HttpListenerResponse response, HttpListener listener)
        {
            _response = response;
            StatusCode = 200; // 默认状态码为200，表示客户端请求已成功
        }
        public Stream ResponseStream
            => _response.OutputStream;

        public string ContentType
        {
            get => _response.ContentType;
            set => _response.ContentType = value;
        }

        public int StatusCode
        {
            get => _response.StatusCode;
            set => _response.StatusCode = value;
        }

        public Encoding ContentEncoding
        {
            get => _response.ContentEncoding;
            set => _response.ContentEncoding = value;
        }

        public long ContentLength64 { get => _response.ContentLength64; set => _response.ContentLength64 = value; }

        public CookieCollection Cookies { get => _response.Cookies; set => _response.Cookies = value; }

        public WebHeaderCollection Headers { get => _response.Headers; set => _response.Headers = value; }

        public bool KeepAlive { get => _response.KeepAlive; set => _response.KeepAlive = value; }

        public Version ProtocolVersion { get => _response.ProtocolVersion; set => _response.ProtocolVersion = value; }

        public string RedirectLocation { get => _response.RedirectLocation; set => _response.RedirectLocation = value; }

        public bool SendChunked { get => _response.SendChunked; set => _response.SendChunked = value; }

        public string StatusDescription { get => _response.StatusDescription; set => _response.StatusDescription = value; }

        public Action Abort => _response.Abort;
        public Action<string, string> AddHeader => _response.AddHeader;
        public Action<Cookie> AppendCookie => _response.AppendCookie;
        public Action<string, string> AppendHeader => _response.AppendHeader;
        public Action Close => _response.Close;
        public Action<byte[], bool> CloseBytes => _response.Close;
        public Action<string> Redirect => _response.Redirect;
        public Action<Cookie> SetCookie => _response.SetCookie;
    }
}
