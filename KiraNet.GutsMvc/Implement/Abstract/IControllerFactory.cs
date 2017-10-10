using System.Reflection;

namespace KiraNet.GutsMvc.Implement
{
    public interface IControllerFactory
    {
        /// <summary>
        /// 创建Controller
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Controller CreateController(ControllerContext context);

        void DisposeController(ControllerContext context, Controller controller);
    }
}
