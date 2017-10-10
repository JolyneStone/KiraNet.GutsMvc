using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 用于描述与请求相关的特性，与HttpRequest具有类型的结构
    /// </summary>
    public interface IHttpRequestFeature
    {
        ///// <summary>
        ///// HTTP发送的数据
        ///// </summary>
        //IFormFeature Form { get; }
        /// <summary>
        /// 当前请求地址
        /// </summary>
        Uri Url { get; }
        /// <summary>
        /// 基地址
        /// </summary>
        string PathBase { get; }
        /// <summary>
        /// 包含GET或POST请求中包含的查询数据
        /// </summary>
        NameValueCollection QueryString { get; }
        /// <summary>
        /// 包含POST请求时发送的数据
        /// </summary>
        //NameValueCollection Form { get; }
        /// <summary>
        /// 指示是否应该保持连接的打开状态
        /// </summary>
        bool KeepAlive { get; }
        /// <summary>
        /// 指示当前请求是否由关联的正文数据
        /// </summary>
        bool HasEntityBody { get; }
        /// <summary>
        /// 标识HTTP协议的版本
        /// </summary>
        Version ProtocolVersion { get; }
        /// <summary>
        /// Cookie集合
        /// </summary>
        CookieCollection Cookies { get; }
        /// <summary>
        /// 一个有关传输层信息的TransportContext对象
        /// </summary>
        TransportContext TransportContext { get; }
        /// <summary>
        /// 标识有问题的错误代码
        /// 异常：T:System.InvalidOperationException:
        ///     客户端证书尚未初始化尚未通过调用 System.Net.HttpListenerRequest.BeginGetClientCertificate(System.AsyncCallback,System.Object)
        ///     或 System.Net.HttpListenerRequest.GetClientCertificate 方法- 或 - 此操作仍然正在进行。
        /// </summary>
        int ClientCertificateError { get; }
        /// <summary>
        /// 标识响应的首选语言数组
        /// 如果为null，则当前请求不包含AcceptLanguage标头
        /// </summary>
        string[] UserLanguager { get; }
        /// <summary>
        /// Host标头
        /// </summary>
        string UserHostName { get; }
        /// <summary>
        /// 请求定向到的IP地址和端口号
        /// </summary>
        string UserHostAddress { get; }
        /// <summary>
        /// 用户代理
        /// </summary>
        string UserAgent { get; }
        /// <summary>
        /// 统一资源标识符
        /// 如果为null，则referer标头未包含在请求中
        /// </summary>
        Uri UrlReferrer { get; }
        /// <summary>
        /// 
        /// </summary>
        string ServiceName { get; }
        /// <summary>
        /// 原始URL信息（不包含主机和端口）
        /// </summary>
        string RawUrl { get; }
        /// <summary>
        /// 发出请求的IP地址和端口号
        /// </summary>
        IPEndPoint RemoteEndPoint { get; }
        /// <summary>
        /// 服务器接收请求定向到的IP地址和端口
        /// </summary>
        IPEndPoint LocalEndPoint { get; }
        /// <summary>
        /// 指示是否使用将请求发送的TCP连接使用的安全套接字层协议（SSL）
        /// </summary>
        bool IsSecureConnection { get; }
        /// <summary>
        /// 指示是否请求来自本地计算机
        /// </summary>
        bool IsLocal { get; }
        /// <summary>
        /// 指示是否对发送此请求的客户端进行身份验证
        /// </summary>
        bool IsAuthenticated { get; }
        /// <summary>
        /// 获取客户端发送的正文数据的流
        /// </summary>
        Stream RequestStream { get; set; }

        string HttpMethod { get; }
        /// <summary>
        /// 请求中发送的标头名称/值对的集合
        /// </summary>
        NameValueCollection Headers { get; }
        /// <summary>
        /// 请求中的正文数据的MIME类型
        /// </summary>
        string ContentType { get; }
        /// <summary>
        /// 包含在请求中的正文数据的长度
        /// 如果不知道的内容长度，则此值为-1
        /// </summary>
        long ContentLength64 { get; }
        /// <summary>
        /// 获取内容编码，可配合RequestStream属性使用
        /// </summary>
        Encoding ContentEncoding { get; }
        /// <summary>
        /// 客户端接受的MIME类型
        /// 如果为null，则当前请求不包括Accept标头
        /// </summary>
        string[] AcceptTypes { get; }
        /// <summary>
        /// HTTP请求的标识符
        /// </summary>
        Guid RequestTraceIdentifier { get; }
        /// <summary>
        /// 指示TCP连接是否是WebSocket
        /// </summary>
        bool IsWebSocketRequest { get; }
        /// <summary>
        /// 开始对客户端的X.509 v.3证书的异步请求
        /// </summary>
        Func<AsyncCallback, object, IAsyncResult> BeginGetClientCertificate { get; }
        /// <summary>
        /// 结束对客户端的X.509 v.3 证书的异步请求
        /// </summary>
        Func<IAsyncResult, X509Certificate2> EndGetClientCertificate { get; }
        /// <summary>
        /// 检索客户端的X.509 v.3 证书
        /// </summary>
        Func<X509Certificate2> GetClientCerificate { get; }
        /// <summary>
        /// 以异步操作检索客户端的X509 v.3 证书
        /// </summary>
        Func<Task<X509Certificate2>> GetClientCertificateAsync { get; }
    }
}
