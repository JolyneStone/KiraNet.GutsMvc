using System;
using System.Threading.Tasks;

namespace KiraNet.GutsMVC
{
    /// <summary>
    /// <see cref="IHttpApplication{TContext}"/> 表示框架针对HTTP请求的方法集合
    /// 该接口可看成是对一系列中间件的组合
    /// 注：MVC请求处理管道由一个服务器（IServer）和本接口组成
    /// </summary>
    /// <typeparam name="TContext">表示针对每次HTTP请求的上下文</typeparam>
    public interface IHttpApplication<TContext> where TContext : Context
    {
        /// <summary>
        /// 创建上下文
        /// </summary>
        /// <param name="contextFeatures">IFeatureCollection接口用于描述一组特性，可看作是一个Dictionary<Type, object></param>
        /// <returns></returns>
        TContext CreateContext(IFeatureCollection contextFeatures);
        /// <summary>
        /// 针对每次HTTP请求进行处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ProcessRequestAsync(TContext context);
        void DisposeContext(TContext context, Exception exception);
    }
}
