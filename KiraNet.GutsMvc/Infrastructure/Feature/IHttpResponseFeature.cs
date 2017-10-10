using System;
using System.IO;
using System.Net;
using System.Text;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 用于描述与响应相关的特性，与HttpResponse有类似的结构
    /// </summary>
    public interface IHttpResponseFeature
    {/// <summary>
     /// 响应流，用于输出页面
     /// </summary>
        Stream ResponseStream { get; }
        /// <summary>
        /// 媒体类型
        /// </summary>
        string ContentType { get; set; }
        /// <summary>
        /// 响应状态码
        /// </summary>
        int StatusCode { get; set; }
        /// <summary>
        /// 获取或设置包含在响应的正文数据中的字节数
        /// </summary>
        long ContentLength64 { get; set; }
        /// <summary>
        /// 获取或设置是否响应使用chunked的传输编码
        /// </summary>
        bool SendChunked { get; set; }
        /// <summary>
        /// 获取或设置由服务器返回的标头名称/值对的集合
        /// </summary>
        WebHeaderCollection Headers { get; set; }
        /// <summary>
        /// 获取或设置一个值，指示服务器是否保持请求的持续性连接
        /// </summary>
        bool KeepAlive { get; set; }
        /// <summary>
        /// 获取或设置与响应一起返回的cookie集合
        /// </summary>
        CookieCollection Cookies { get; set; }
        /// <summary>
        /// 获取或设置返回到客户端的HTTP状态代码的文本说明
        /// 默认值时RFC 2616描述，若属性值为空字符串，则说明不存在RFC 2616
        /// </summary>
        string StatusDescription { get; set; }
        /// <summary>
        /// 获取或设置包含要发送到客户端的HTTP的绝对URL Location标头
        /// </summary>
        string RedirectLocation { get; set; }
        /// <summary>
        /// 获取或折hi用于响应的HTTP版本，此属性已过时
        /// </summary>
        Version ProtocolVersion { get; set; }
        /// <summary>
        /// 获取或设置响应正文数据的编码，可配合ResponseStream使用
        /// </summary>
        Encoding ContentEncoding { get; set; }
        /// <summary>
        /// 关闭到客户端的连接而不发生响应
        /// </summary>
        Action Abort { get; }
        /// <summary>
        /// 将指定的标头和值添加到此响应的HTTP标头
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        Action<string, string> AddHeader { get; }
        /// <summary>
        /// 添加指定Cookie到此响应集合
        /// </summary>
        /// <param name="cookie"></param>
        Action<Cookie> AppendCookie { get; }
        /// <summary>
        /// 将一个值追加到指定的HTTP标头
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        Action<string, string> AppendHeader { get; }
        /// <summary>
        /// 发送到客户端并释放资源
        /// </summary>
        Action Close { get; }
        /// <summary>
        /// 将指定的字节数组返回客户端并释放资源
        /// </summary>
        /// <param name="responseEntity">包含要发送到客户端的响应</param>
        /// <param name="willBlock">true：为了阻止客户端，则为刷新流时执行，否则为false</param>
        Action<byte[], bool> CloseBytes { get;}
        /// <summary>
        /// 配置要将客户端重定向到指定的URL响应
        /// </summary>
        /// <param name="url"></param>
        Action<string> Redirect { get; }
        /// <summary>
        /// 添加或更新Cookie中与此响应发送的 cookie 集合。
        /// </summary>
        /// <param name="cookie"></param>
        Action<Cookie> SetCookie { get; }
    }
}
