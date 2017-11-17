using KiraNet.GutsMvc.Route;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public abstract class HttpRequest
    {
        public abstract HttpContext HttpContext { get; }

        /// <summary>
        /// 当前请求地址
        /// </summary>
        public abstract Uri Url { get; }
        /// <summary>
        /// 基地址
        /// </summary>
        public abstract string PathBase { get; }
        public abstract RouteEntity RouteEntity { get; internal set; }
        //public abstract RouteData RouteData { get; internal set; }
        /// <summary>
        /// 包含请求中包含的查询数据
        /// </summary>
        public abstract NameValueCollection QueryString { get; }
        /// <summary>
        /// 重定位URL中的查询数据
        /// </summary>
        public abstract NameValueCollection RedirectQueryString { get; internal set; }
        /// <summary>
        /// 指示是否应该保持连接的打开状态
        /// </summary>
        public abstract bool KeepAlive { get; }
        /// <summary>
        /// 指示当前请求是否由关联的正文数据
        /// </summary>
        public abstract bool HasEntityBody { get; }
        /// <summary>
        /// 标识HTTP协议的版本
        /// </summary>
        public abstract Version ProtocolVersion { get; }
        /// <summary>
        /// Cookie集合
        /// </summary>
        public abstract CookieCollection Cookies { get; }
        /// <summary>
        /// 一个有关传输层信息的TransportContext对象
        /// </summary>
        public abstract TransportContext TransportContext { get; }
        /// <summary>
        /// 标识有问题的错误代码
        /// 异常：T:System.InvalidOperationException:
        ///     客户端证书尚未初始化尚未通过调用 System.Net.HttpListenerRequest.BeginGetClientCertificate(System.AsyncCallback,System.Object)
        ///     或 System.Net.HttpListenerRequest.GetClientCertificate 方法- 或 - 此操作仍然正在进行。
        /// </summary>
        public abstract int ClientCertificateError { get; }
        /// <summary>
        /// 标识响应的首选语言数组
        /// 如果为null，则当前请求不包含AcceptLanguage标头
        /// </summary>
        public abstract string[] UserLanguager { get; }
        /// <summary>
        /// Host标头
        /// </summary>
        public abstract string UserHostName { get; }
        /// <summary>
        /// 请求定向到的IP地址和端口号
        /// </summary>
        public abstract string UserHostAddress { get; }
        /// <summary>
        /// 用户代理
        /// </summary>
        public abstract string UserAgent { get; }
        /// <summary>
        /// 统一资源标识符
        /// 如果为null，则referer标头未包含在请求中
        /// </summary>
        public abstract Uri UrlReferrer { get; }
        /// <summary>
        /// 
        /// </summary>
        public abstract string ServiceName { get; }
        /// <summary>
        /// 原始URL信息（不包含主机和端口）
        /// </summary>
        public abstract string RawUrl { get; }
        /// <summary>
        /// 发出请求的IP地址和端口号
        /// </summary>
        public abstract IPEndPoint RemoteEndPoint { get; }
        /// <summary>
        /// 服务器接收请求定向到的IP地址和端口
        /// </summary>
        public abstract IPEndPoint LocalEndPoint { get; }
        /// <summary>
        /// 指示是否使用将请求发送的TCP连接使用的安全套接字层协议（SSL）
        /// </summary>
        public abstract bool IsSecureConnection { get; }
        /// <summary>
        /// 指示是否请求来自本地计算机
        /// </summary>
        public abstract bool IsLocal { get; }
        /// <summary>
        /// 指示是否对发送此请求的客户端进行身份验证
        /// </summary>
        public abstract bool IsAuthenticated { get; }
        /// <summary>
        /// 获取客户端发送的正文数据的流
        /// </summary>
        public abstract Stream RequestStream { get; set; }
        /// <summary>
        /// 获取HTTP方法类型
        /// </summary>
        public abstract string HttpMethod { get; }
        /// <summary>
        /// 请求中发送的标头名称/值对的集合
        /// </summary>
        public abstract NameValueCollection Headers { get; }
        /// <summary>
        /// 请求中的正文数据的MIME类型
        /// </summary>
        public abstract string ContentType { get; }
        /// <summary>
        /// 包含在请求中的正文数据的长度
        /// 如果不知道的内容长度，则此值为-1
        /// </summary>
        public abstract long ContentLength64 { get; }
        /// <summary>
        /// 获取内容编码，可配合RequestStream属性使用
        /// </summary>
        public abstract Encoding ContentEncoding { get; }
        /// <summary>
        /// 客户端接受的MIME类型
        /// 如果为null，则当前请求不包括Accept标头
        /// </summary>
        public abstract string[] AcceptTypes { get; }
        /// <summary>
        /// HTTP请求的标识符
        /// </summary>
        public abstract Guid RequestTraceIdentifier { get; }
        /// <summary>
        /// 指示TCP连接是否是WebSocket
        /// </summary>
        public abstract bool IsWebSocketRequest { get; }
        /// <summary>
        /// form中的数据
        /// </summary>
        public abstract IFormCollection Form { get; internal set; }
        /// <summary>
        /// 开始对客户端的X.509 v.3证书的异步请求
        /// </summary>
        /// <param name="requestCallback">操作完成时要调用的方法</param>
        /// <param name="state">传入回调委托的相关信息</param>
        /// <returns></returns>
        public abstract IAsyncResult BeginGetClientCertificate(AsyncCallback requestCallback, object state);
        /// <summary>
        /// 结束对客户端的X.509 v.3 证书的异步请求
        /// </summary>
        /// <param name="asyncResult">挂起的请求的证书</param>
        /// <returns>操作开始时返回的对象</returns>
        public abstract X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult);
        /// <summary>
        /// 检索客户端的X.509 v.3 证书
        /// </summary>
        /// <returns></returns>
        public abstract X509Certificate2 GetClientCerificate();
        /// <summary>
        /// 以异步操作检索客户端的X509 v.3 证书
        /// </summary>
        /// <returns>客户端的X.509 v.3</returns>
        public abstract Task<X509Certificate2> GetClientCertificateAsync();
    }
}

