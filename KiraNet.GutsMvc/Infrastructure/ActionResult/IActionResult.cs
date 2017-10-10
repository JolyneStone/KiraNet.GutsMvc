using KiraNet.GutsMvc.Implement;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public interface IActionResult
    {
        void ExecuteResult(ControllerContext context);
        Task ExecuteResultAsync(ControllerContext context);
    }
}
