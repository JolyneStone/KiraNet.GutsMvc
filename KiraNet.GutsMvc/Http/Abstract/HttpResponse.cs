using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMVC
{
    public abstract class HttpResponse
    {
        private readonly IList<Func<Func<Task>, Func<Task>>> _callbackCompleting = new List<Func<Func<Task>, Func<Task>>>();
        private readonly IList<Func<Func<Task>, Func<Task>>> _callbackCompleted = new List<Func<Func<Task>, Func<Task>>>();
        private readonly IList<Func<Action, Action>> _callbackDispose = new List<Func<Action, Action>>();
        //private readonly IList<KeyValuePair<object, Func<object, Task>>> _callbackCompleting = new List<KeyValuePair<object, Func<object, Task>>>();
        //private readonly IList<KeyValuePair<object, Func<object, Task>>> _callbackCompled = new List<KeyValuePair<object, Func<object, Task>>>();
        //private readonly IList<KeyValuePair<IDisposable, Action<IDisposable>>> _disposeDelegate = new List<KeyValuePair<IDisposable, Action<IDisposable>>>();
        public abstract HttpContext HttpContext { get; }
        /// <summary>
        /// 响应流，用于输出页面
        /// </summary>
        public abstract Stream ResponseStream { get; }
        /// <summary>
        /// 媒体类型
        /// </summary>
        public abstract string ContentType { get; set; }
        /// <summary>
        /// 响应状态码
        /// </summary>
        public abstract int StatusCode { get; set; }
        /// <summary>
        /// 获取或设置包含在响应的正文数据中的字节数
        /// </summary>
        public abstract long ContentLength64 { get; set; }
        /// <summary>
        /// 获取或设置是否响应使用chunked的传输编码
        /// </summary>
        public abstract bool SendChunked { get; set; }
        /// <summary>
        /// 获取或设置由服务器返回的标头名称/值对的集合
        /// </summary>
        public abstract WebHeaderCollection Headers { get; set; }
        /// <summary>
        /// 获取或设置一个值，指示服务器是否保持请求的持续性连接
        /// </summary>
        public abstract bool KeepAlive { get; set; }
        /// <summary>
        /// 获取或设置与响应一起返回的cookie集合
        /// </summary>
        public abstract CookieCollection Cookies { get; set; }
        /// <summary>
        /// 获取或设置返回到客户端的HTTP状态代码的文本说明
        /// 默认值时RFC 2616描述，若属性值为空字符串，则说明不存在RFC 2616
        /// </summary>
        public abstract string StatusDescription { get; set; }
        /// <summary>
        /// 获取或设置包含要发送到客户端的HTTP的绝对URL Location标头
        /// </summary>
        public abstract string RedirectLocation { get; set; }
        /// <summary>
        /// 获取或折hi用于响应的HTTP版本，此属性已过时
        /// </summary>
        public abstract Version ProtocolVersion { get; set; }
        /// <summary>
        /// 获取或设置响应正文数据的编码，可配合ResponseStream使用
        /// </summary>
        public abstract Encoding ContentEncoding { get; set; }
        /// <summary>
        /// 关闭到客户端的连接而不发生响应
        /// </summary>
        public abstract void Abort();
        /// <summary>
        /// 将指定的标头和值添加到此响应的HTTP标头
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void AddHeader(string name, string value);
        /// <summary>
        /// 添加指定Cookie到此响应集合
        /// </summary>
        /// <param name="cookie"></param>
        public abstract void AppendCookie(Cookie cookie);
        /// <summary>
        /// 将一个值追加到指定的HTTP标头
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public abstract void AppendHeader(string name, string value);
        /// <summary>
        /// 发送到客户端并释放资源
        /// </summary>
        public abstract void Close();
        /// <summary>
        /// 将指定的字节数组返回客户端并释放资源
        /// </summary>
        /// <param name="responseEntity">包含要发送到客户端的响应</param>
        /// <param name="willBlock">true：为了阻止客户端，则为刷新流时执行，否则为false</param>
        public abstract void Close(byte[] responseEntity, bool willBlock);
        /// <summary>
        /// 配置要将客户端重定向到指定的URL响应
        /// </summary>
        /// <param name="url"></param>
        public abstract void Redirect(string url);
        /// <summary>
        /// 添加或更新Cookie中与此响应发送的 cookie 集合。
        /// </summary>
        /// <param name="cookie"></param>
        public abstract void SetCookie(Cookie cookie);

        public virtual void OnCompleting(Func<Func<Task>, Func<Task>> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));
            _callbackCompleting.Add(callback);
        }
        public virtual void OnCompleting(Func<Task> callback) => OnCompleting(last => { last(); return callback; });

        public  virtual void OnCompleted(Func<Task> callback) => OnCompleted(callback: last => { last(); return callback; });
        public virtual void OnCompleted(Func<Func<Task>, Func<Task>> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));
            _callbackCompleted.Add(callback);
        }

        public virtual void OnDispose(Func<Action, Action> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            _callbackDispose.Add(callback);
        }

        public virtual void RegisterForDispose(IDisposable disposable)
        {
            if (disposable == null)
                throw new ArgumentNullException(nameof(disposable));
            OnDispose(last => () => { last(); disposable.Dispose(); });
        }

        public virtual void OnDispose(Action callback)
            => OnDispose(last => { last(); return callback; });

        protected virtual void Completing()
        {
            if (_callbackCompleting == null || _callbackCompleting.Count == 0)
                return;
            Func<Task> seed = () => Task.Run(() => { });
            var delegates = _callbackCompleting.Aggregate(seed, (next, current) => current(next))();
        }

        protected virtual void Completed()
        {
            if (_callbackCompleted == null || _callbackCompleted.Count == 0)
                return;

            Func<Task> seed = () => Task.Run(() => { });
            _callbackCompleting.Aggregate(seed, (next, current) => current(next))();
        }

        protected virtual void Disposable()
        {
            if (_callbackDispose == null || _callbackDispose.Count == 0)
                return;

            Action seed = () => { };
            _callbackDispose.Aggregate(seed, (next, current) => current(next))();
        }
    }
}
