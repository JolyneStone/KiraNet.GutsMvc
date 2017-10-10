using System;

namespace KiraNet.GutsMvc.MvcSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseHttpListener()
                .UseUrls("http://+:17758/")
                .UseStartup<Startup>()
                //.Configure(app =>
                //{
                    //app.UseImages(@"C:\Users\99752\Pictures"); // 注册中间件
                //})
                .Build()
                .Start();

            Console.ReadKey();
        }
    }
}
