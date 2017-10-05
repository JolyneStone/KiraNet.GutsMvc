using KiraNet.GutsMVC.Metadata;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace KiraNet.GutsMVC
{
    public class WebHost : IWebHost
    {
        private IServiceProvider _serviceProvider;
        private IConfiguration _config;

        public WebHost(IServiceCollection services, IConfiguration config)
        {
            _serviceProvider = services.BuildServiceProvider();
            _config = config;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // 支持中文编码
        }

        public void Start()
        {
            // 真正完成中间件注册
            IApplicationBuilder applicationBuilder = _serviceProvider.GetRequiredService<IApplicationBuilder>();
            _serviceProvider.GetRequiredService<IApplicationStartup>().Configure(applicationBuilder);

            IServer server = _serviceProvider.GetRequiredService<IServer>();
            IServerAddressesFeature addressFeatures = server.Features.Get<IServerAddressesFeature>();

            string addresses = _config["ServerAddresses"] ?? "http://localhost:17758";
            foreach (string address in addresses.Split(';'))
            {
                addressFeatures.Addresses.Add(address);
            }

            new DefaultModelMetadataProvider(_serviceProvider.GetRequiredService<IMemoryCache>());

            server.Run(new HostingApplication(applicationBuilder.Build(), _serviceProvider));
        }
    }
}
