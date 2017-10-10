using KiraNet.GutsMvc.Implement;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// EmptyResult表示不对请求做出响应，即我们只接收请求而不发送任何有效的信息
    /// </summary>
    public class EmptyResult : IActionResult
    {
        public void ExecuteResult(ControllerContext context)
        {
        }

        public Task ExecuteResultAsync(ControllerContext context)
        {
            return Task.CompletedTask;
        }
    }
}
