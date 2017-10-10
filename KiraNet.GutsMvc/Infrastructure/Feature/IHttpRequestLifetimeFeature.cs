using System.Threading;

namespace KiraNet.GutsMvc
{
    public interface IHttpRequestLifetimeFeature
    {

        CancellationToken RequestAborted { get; set; }

        void Abort();
    }
}