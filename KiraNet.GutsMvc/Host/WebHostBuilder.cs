using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KiraNet.GutsMvc
{
    public class WebHostBuilder : IWebHostBuilder
    {
        private readonly IServiceCollection _services;
        /// <summary>
        /// 配置源，默认配置源类型为内存变量
        /// </summary>
        private readonly IConfiguration _config;
        public IServiceCollection Services => _services;
        public WebHostBuilder()
        {
            /// 注册IApplicationBuilder
            _services = new ServiceCollection();
                //.AddMemoryCache()
                //.ConfigureDefaultInject();

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();
        }

        public IWebHost Build()
            => new WebHost(_services, _config);

        public IWebHostBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            configureServices(_services); // 由于configureServices委托的委托体是对ApplicationStartup的初始化和DI注册，而ApplicationStartup是对注册中间件方法的封装
            return this;
        }

        public IWebHostBuilder UseSetting(string key, string value)
        {
            _config[key] = value;
            return this;
        }
    }
}
