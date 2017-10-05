using System.Threading;

namespace KiraNet.GutsMVC
{
    public interface IHttpRequestLifetimeFeature
    {

        CancellationToken RequestAborted { get; set; }

        void Abort();
    }
}