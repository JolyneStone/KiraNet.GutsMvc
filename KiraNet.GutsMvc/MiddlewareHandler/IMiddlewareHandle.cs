using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 中间件处理接口
    /// </summary>
    public interface IMiddlewareHandle
    {
        Task MiddlewareExecute(HttpContext httpContext);
    }
}
