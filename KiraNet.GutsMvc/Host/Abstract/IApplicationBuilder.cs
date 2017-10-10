using System;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// IHttpApplication的建造者
    /// 注：WEB请求管道由一个服务器和一个HttpApplication构成
    /// 服务器负责监听和传递请求给HttpAplication对象，后者通过调用由中间件处理程序的委托链来处理请求
    /// 所以，本接口的目的就是为了提供注册中间件的注册，和建造HttpApplication
    /// </summary>
    public interface IApplicationBuilder
    {
        /// <summary>
        /// 构造RequestDelegate委托链
        /// </summary>
        /// <returns></returns>
        Func<HttpContext, Task> Build();
        IApplicationBuilder Use(Func<Func<HttpContext, Task>, Func<HttpContext, Task>> middleware);
    }
}
