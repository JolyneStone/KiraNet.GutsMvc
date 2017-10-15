using Microsoft.Extensions.DependencyInjection;
using System;

namespace KiraNet.GutsMvc
{
    public class ApplicationStartup : IApplicationStartup
    {
        private Action<IApplicationBuilder> _configure;
        private IServiceCollection _services;

        /// <summary>
        /// 注册中间件的委托
        /// </summary>
        /// <param name="configure">对中间件的注册委托</param>
        public ApplicationStartup(IServiceCollection services, Action<IApplicationBuilder> configure)
        {
            _services = services;
            this._configure = configure;
        }

        public void Configure(IApplicationBuilder app)
            => this._configure(app);
    }
}
