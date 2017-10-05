using KiraNet.GutsMVC.Implement;
using System.Threading.Tasks;

namespace KiraNet.GutsMVC
{
    public interface IActionResult
    {
        void ExecuteResult(ControllerContext context);
        Task ExecuteResultAsync(ControllerContext context);
    }
}
