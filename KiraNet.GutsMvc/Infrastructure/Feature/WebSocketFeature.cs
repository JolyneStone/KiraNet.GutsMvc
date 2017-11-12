using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public class WebSocketFeature : IWebSocketFeature
    {
        // int默认值为16385， TimeSpan默认值为30秒
        private Func<string, int, TimeSpan, Task<HttpListenerWebSocketContext>> _getWebSocketAsync { get; }

        public WebSocketFeature(Func<string, int, TimeSpan, Task<HttpListenerWebSocketContext>> getWebSocketAsync)
        {
            _getWebSocketAsync = getWebSocketAsync ?? throw new ArgumentNullException(nameof(getWebSocketAsync));
        }

        public async Task<HttpListenerWebSocketContext> GetWebSocketAsync(string subProtocol)
        {
            return await _getWebSocketAsync(subProtocol, 16385,  new TimeSpan(0, 0, 30));
        }

        public async Task<HttpListenerWebSocketContext> GetWebSocketAsync(string subProtocol, int receiveBufferSize)
        {
            return await _getWebSocketAsync(subProtocol, receiveBufferSize, new TimeSpan(0, 0, 30));
        }

        public async Task<HttpListenerWebSocketContext> GetWebSocketAsync(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval)
        {
            return await _getWebSocketAsync(subProtocol, receiveBufferSize, keepAliveInterval);
        }
    }
}
