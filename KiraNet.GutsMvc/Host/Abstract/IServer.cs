namespace KiraNet.GutsMvc
{
    /// <summary>
    /// WEB框架服务器
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// 在启动后，HTTP请求一旦抵达，就利用IHttpApplication对象创建一个上下文，并在此上下文中完成对请求的所有处理操作，上下文由HttpApplication负责回收
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="application"></param>
        void Run<TContext>(IHttpApplication<TContext> application) where TContext : Context;
        /// <summary>
        /// 描述服务器特性的集合
        /// </summary>
        IFeatureCollection Features { get; }
    }
}
