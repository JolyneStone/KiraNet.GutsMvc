using System.Threading;

namespace KiraNet.GutsMvc
{
    public class HttpContextCache : IHttpContextCache
    {
        /// AsyncLocal能够在异步方法进行线程切换时保持同一实例
        private AsyncLocal<HttpContext> _currentContext = new AsyncLocal<HttpContext>();

        public HttpContext HttpContext
        {
            get
            {
                return _currentContext.Value;
            }
            set
            {
                _currentContext.Value = value;
            }
        }
    }
}
