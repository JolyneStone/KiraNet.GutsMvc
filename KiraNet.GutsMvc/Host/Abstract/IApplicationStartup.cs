namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 用于应用启动时的初始化工作，包括中间件的注册以及管道的构建
    /// </summary>
    public interface IApplicationStartup
    {
        /// <summary>
        /// 为管道注册中间件
        /// </summary>
        /// <param name="app"></param>
        void Configure(IApplicationBuilder app);
    }
}
