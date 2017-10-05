using System;

namespace KiraNet.GutsMVC
{
    /// <summary>
    /// 此上下文是IHttpApplication的泛型参数
    /// 其本质是对当前HTTP请求上下文的封装
    /// </summary>
    public class Context
    {
        public HttpContext HttpContext { get; set; }
        /// <summary>
        /// Scope表示一个上下文范围，用于将多次的日志记录关联到一个Scope，也就是Logger的BeginScope方法返回值。其实我们可以把它和DI中的ServiceProvider结合，这样就可以将DI生命周期管理和HTTP请求关联起来，
        /// </summary>
        public IDisposable Scope { get; set; }
        /// <summary>
        /// 表示开始处理请求的时间戳
        /// </summary>
        public long StartTimestamp { get; set; }
    }
}
