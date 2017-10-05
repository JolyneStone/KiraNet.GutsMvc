namespace KiraNet.GutsMVC.Implement
{
    /// <summary>
    /// 激活Controller
    /// </summary>
    public interface IControllerActivator
    {
        Controller Create(ControllerContext context);
        void Release(ControllerContext context, Controller controller);
    }
}
