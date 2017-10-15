using KiraNet.GutsMvc.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public class ApplicationBuilder : IApplicationBuilder
    {
        /// <summary>
        /// 中间件处理程序集合
        /// </summary>
        private IList<Func<Func<HttpContext, Task>, Func<HttpContext, Task>>> middlewares = new List<Func<Func<HttpContext, Task>, Func<HttpContext, Task>>>();

        public ApplicationBuilder()
        {
            // 将StaticFileHandler作为第一个中间件
            this.Use(new StaticFileHandler());
        }

        public Func<HttpContext, Task> Build()
        {
            // 将中间件处理程序集合转换成一个委托
            Func<HttpContext, Task> seed = context => Task.Run(() => { });
            return middlewares.Aggregate(seed, (next, current) => current(next));
        }

        public IApplicationBuilder Use(Func<Func<HttpContext, Task>, Func<HttpContext, Task>> middleware)
        {
            if (middleware == null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            middlewares.Add(middleware);
            return this;
        }
    }
}
