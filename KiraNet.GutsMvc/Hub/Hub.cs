using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.WebSocketHub
{
    public abstract class Hub
    {
        internal System.Net.WebSockets.WebSocket WebSocket { get; set; }

        public HttpContext HttpContext { get; set; }

        public WebSocketState State => WebSocket.State;

        internal async Task Process()
        {
            bool isNormalEnd = true;
            await OnContentedAsync();

            while (WebSocket != null && State == WebSocketState.Open)
            {
                try
                {
                    var message = await WebSocket.ReceiveAsync();
                    await OnReceiveAsync(message);
                }
                catch
                {
                    isNormalEnd = false;
                    await OnDisconnectedAsync(isNormalEnd);
                    break;
                }
            }

            HttpContext.Response.StatusCode = (int)(isNormalEnd ?
                System.Net.HttpStatusCode.OK :
                System.Net.HttpStatusCode.InternalServerError);
        }

        public virtual Task OnReceiveAsync(string message)
        {
            return Task.CompletedTask;
        }

        public virtual async Task OnSendAsync(string message)
        {
            if (State == WebSocketState.Open)
                await WebSocket.SendAsync(message);
        }

        public virtual Task OnContentedAsync()
        {
            return Task.CompletedTask;
        }

        public virtual async Task OnDisconnectedAsync(bool isNormalClose)
        {
            if (WebSocket != null && WebSocket.State == WebSocketState.Open)
            {
                await WebSocket.CloseAsync(
                    isNormalClose ?
                        WebSocketCloseStatus.NormalClosure :
                        WebSocketCloseStatus.InternalServerError,
                    "websocket close",
                    CancellationToken.None);
            }
        }
    }
}