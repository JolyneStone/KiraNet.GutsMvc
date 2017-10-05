using System.Threading;

namespace KiraNet.GutsMVC
{
    public class HttpRequestLifetimeFeature : IHttpRequestLifetimeFeature
    {
        public CancellationToken RequestAborted { get; set; }

        public void Abort()
        {
        }
    }
}