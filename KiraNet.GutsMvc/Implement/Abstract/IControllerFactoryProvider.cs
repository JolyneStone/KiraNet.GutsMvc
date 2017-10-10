using System;

namespace KiraNet.GutsMvc.Implement
{
    //public interface IControllerFactoryProvider
    //{
    //    /// <summary>
    //    /// 创建IControllerFactory的委托
    //    /// </summary>
    //    /// <param name="descriptor"></param>
    //    /// <returns></returns>
    //    Func<ControllerContext, Controller> CreateControllerFactory(ControllerActionDescriptor descriptor);

    //    /// <summary>
    //    /// 创建释放Controller的委托
    //    /// </summary>
    //    /// <param name="descriptor"></param>
    //    /// <returns></returns>

    //    Action<ControllerContext, Controller> CreateControllerReleaser(ControllerActionDescriptor descriptor);
    //}

    public interface IControllerFactoryProvider
    {
        /// <summary>
        /// 创建IControllerFactory的委托
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        Func<ControllerContext, Controller> CreateControllerFactory(ControllerDescriptor descriptor);

        /// <summary>
        /// 创建释放Controller的委托
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>

        Action<ControllerContext, Controller> CreateControllerReleaser(ControllerDescriptor descriptor);
    }
}
