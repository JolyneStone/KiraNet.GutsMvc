using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public interface IWebSocketFeature
    {
        //Func<string, int, TimeSpan, Task<HttpListenerWebSocketContext>> GetWebSocketAsync { get; }
        Task<HttpListenerWebSocketContext> GetWebSocketAsync(string subProtocol);
        Task<HttpListenerWebSocketContext> GetWebSocketAsync(string subProtocol, int receiveBufferSize);
        Task<HttpListenerWebSocketContext> GetWebSocketAsync(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval);
    }
}
