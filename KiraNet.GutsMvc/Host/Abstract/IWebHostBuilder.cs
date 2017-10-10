using Microsoft.Extensions.DependencyInjection;
using System;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 用于创建WebHost宿主
    /// </summary>
    public interface IWebHostBuilder
    {
        /// <summary>
        /// 创建宿主
        /// </summary>
        /// <returns></returns>
        IWebHost Build();
        /// <summary>
        /// 采用依赖注入的形式为WebHost的创建提供信息
        /// </summary>
        /// <param name="configureServices"></param>
        /// <returns></returns>
        IWebHostBuilder ConfigureServices(Action<IServiceCollection> configureServices);
        /// <summary>
        /// 为WebHost提供一些额外的配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IWebHostBuilder UseSetting(string key, string value);

        IServiceCollection Services { get; }
    }
}
