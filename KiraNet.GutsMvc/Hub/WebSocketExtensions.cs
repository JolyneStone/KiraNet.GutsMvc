using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.WebSocketHub
{
    public static class WebSocketExtensions
    {
        public static async Task<string> ReceiveAsync(this WebSocket webSocket)
        {
            return await ReceiveAsync(webSocket, CancellationToken.None);
        }
        public static async Task<string> ReceiveAsync(this WebSocket webSocket, CancellationToken cancellation)
        {
            string message = String.Empty;
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer, cancellation);
            if (webSocket.State == WebSocketState.Open)
            {
                message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
            }
            else
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "websocket close", CancellationToken.None);
                message = String.Empty;
            }

            return message;
        }

        public static async Task<bool> SendAsync(this WebSocket webSocket, string message)
        {
            return await SendAsync(webSocket, message, CancellationToken.None);
        }

        public static async Task<bool> SendAsync(this WebSocket webSocket, string message, CancellationToken cancellation)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message", nameof(message));
            }

            if(webSocket.State == WebSocketState.Open)
            {
                var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellation);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
