using System;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.WebSocketHub
{
    internal interface IHubProvider
    {
        Task<Hub> GetHub(HttpContext httpContext, Type hubType);
    }
}
