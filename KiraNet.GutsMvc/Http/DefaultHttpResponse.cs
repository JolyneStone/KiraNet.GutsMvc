using System;
using System.IO;
using System.Net;
using System.Text;

namespace KiraNet.GutsMVC
{
    public class DefaultHttpResponse : HttpResponse
    {
        private HttpContext _context;
        public IHttpResponseFeature ResponseFeature { get; }

        public DefaultHttpResponse(DefaultHttpContext context)
        {
            this.ResponseFeature = context.HttpContextFeatures.Get<IHttpResponseFeature>();
            _context = context;
        }

        public override HttpContext HttpContext => _context ?? throw new NullReferenceException(nameof(_context));

        public override Stream ResponseStream
            => this.ResponseFeature.ResponseStream;

        public override string ContentType
        {
            get => this.ResponseFeature.ContentType;
            set => this.ResponseFeature.ContentType = value;
        }

        public override int StatusCode
        {
            get => this.ResponseFeature.StatusCode;
            set => this.ResponseFeature.StatusCode = value;
        }

        public override Encoding ContentEncoding
        {
            get => this.ResponseFeature.ContentEncoding;
            set => ResponseFeature.ContentEncoding = value;
        }

        public override long ContentLength64 { get => ResponseFeature.ContentLength64; set => ResponseFeature.ContentLength64 = value; }

        public override CookieCollection Cookies { get => ResponseFeature.Cookies; set => ResponseFeature.Cookies = value; }

        public override WebHeaderCollection Headers { get => ResponseFeature.Headers; set => ResponseFeature.Headers = value; }

        public override bool KeepAlive { get => ResponseFeature.KeepAlive; set => ResponseFeature.KeepAlive = value; }

        public override Version ProtocolVersion { get => ResponseFeature.ProtocolVersion; set => ResponseFeature.ProtocolVersion = value; }

        public override string RedirectLocation { get => ResponseFeature.RedirectLocation; set => ResponseFeature.RedirectLocation = value; }

        public override bool SendChunked { get => ResponseFeature.SendChunked; set => ResponseFeature.SendChunked = value; }

        public override string StatusDescription { get => ResponseFeature.StatusDescription; set => ResponseFeature.StatusDescription = value; }

        public override void Abort()
        {
            ResponseFeature.Abort();
        }

        public override void AddHeader(string name, string value)
        {
            ResponseFeature.AddHeader(name, value);
        }

        public override void AppendCookie(Cookie cookie)
        {
            ResponseFeature.AppendCookie(cookie);
        }

        public override void AppendHeader(string name, string value)
        {
            ResponseFeature.AppendHeader(name, value);
        }

        public override void Close()
        {
            this.Completing();
            ResponseFeature.Close();
            this.Completed();
            this.Disposable();
        }

        public override void Close(byte[] responseEntity, bool willBlock)
        {
            ResponseFeature.CloseBytes(responseEntity, willBlock);
        }

        public override void Redirect(string url)
        {
            ResponseFeature.Redirect(url);
        }

        public override void SetCookie(Cookie cookie)
        {
            ResponseFeature.SetCookie(cookie);
        }
    }
}
