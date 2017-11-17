using KiraNet.GutsMvc.WebSocketHub;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.MiddlewareHandler
{
    internal sealed class WebSocketHubHandler : IMiddlewareHandle
    {
        public async Task MiddlewareExecute(HttpContext httpContext)
        {
            if (httpContext.IsCancel || !httpContext.Request.IsWebSocketRequest)
                return;

            if(!HubMap.Map.TryGetHub(httpContext.Request.RawUrl, out var hubType))
            {
                httpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return;
            }

            var hub = await httpContext.Service.GetRequiredService<IHubProvider>()
                ?.GetHub(httpContext, hubType);
            if(hub == null)
            {
                httpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            }

            await hub.Process();
        }
    }
}
