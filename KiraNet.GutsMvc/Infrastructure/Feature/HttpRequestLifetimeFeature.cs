using System.Threading;

namespace KiraNet.GutsMvc
{
    public class HttpRequestLifetimeFeature : IHttpRequestLifetimeFeature
    {
        public CancellationToken RequestAborted { get; set; }

        public void Abort()
        {
        }
    }
}