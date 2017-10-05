using KiraNet.GutsMVC.Implement;

namespace KiraNet.GutsMVC.Infrastructure
{
    /// <summary>
    /// <see cref="IValueProviderFactory"/>是创建ValueProvider的抽象工厂
    /// 用抽象工厂实现
    /// </summary>
    public interface IValueProviderFactory
    {
        IValueProvider CreateValueProvider(ControllerContext controllerContext);
    }
}