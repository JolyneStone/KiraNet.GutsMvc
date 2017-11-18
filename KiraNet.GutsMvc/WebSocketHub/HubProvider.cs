using KiraNet.GutsMvc.Infrastructure;
using KiraNet.GutsMvc.ModelBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.WebSocketHub
{
    internal class HubProvider : IHubProvider
    {
        private static string defaultProtocol = "gutsmvc";
        public async Task<Hub> GetHub(HttpContext httpContext, Type hubType)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (hubType == null)
            {
                throw new ArgumentNullException(nameof(hubType));
            }

            string subProtocol = defaultProtocol;
            var protocols = hubType.GetCustomAttributes<HubProtocolAttribute>(true);
            WebSocket webSocket = null;
            if (protocols != null && protocols.Any())
            {
                foreach (var protocol in protocols)
                {
                    (bool result, var tmpWebSocket) = await TryGetWebSocket(httpContext, protocol.SubProtocol);
                    if (result)
                    {
                        webSocket = tmpWebSocket;
                        break;
                    }
                }
            }
            
            if(webSocket == null)
            {
                (bool result, var tmpWebSocket) = await TryGetWebSocket(httpContext, subProtocol);
                if (result)
                {
                    webSocket = tmpWebSocket;
                }
            }

            if (webSocket == null)
            {
                throw new InvalidOperationException($"无法获取到目标WebSocke， 请检查您的subprotocol");
            }

            Hub hub = TryGetHub(httpContext, hubType);
            if(hub!=null)
            {
                hub.HttpContext = httpContext;
                hub.WebSocket = webSocket;
            }

            return hub;
        }

        private static async Task<ValueTuple<bool, WebSocket>> TryGetWebSocket(HttpContext httpContext, string subProtocol)
        {
            try
            {
                var webSocket = (await httpContext.WebStocket.GetWebSocketAsync(subProtocol)).WebSocket;
                return (true, webSocket);
            }
            catch (ArgumentException)
            {
                return (false, null);
            }
            catch (WebSocketException)
            {
                return (false, null);
            }
        }

        private static Hub TryGetHub(HttpContext httpContext, Type hubType)
        {
            Hub hub = null;
            IValueProvider valueProvider = ValueProviderFactories.Factories.GetValueProvider(httpContext);
            IModelBinderInvoker binderInvoker = new DefaultModelBinderInvoker();
            foreach (var constructor in hubType.GetConstructors(
                            BindingFlags.CreateInstance |
                            BindingFlags.Instance |
                            BindingFlags.Public)
                            .OrderByDescending(x => x.GetParameters().Length))
            {
                var parameters = constructor.GetParameters();
                if (parameters == null || parameters.Length == 0)
                {
                    hub = constructor.Invoke(null) as Hub;
                    break;
                }
                else
                {
                    var index = 0;
                    List<object> parameterValues = new List<object>();
                    for (; index < parameters.Length; index++)
                    {
                        if (binderInvoker.TryBindModel(httpContext, valueProvider, parameters[index], out var value))
                        {
                            parameterValues.Add(value);
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (index == parameters.Length)
                    {
                        hub = constructor.Invoke(parameterValues.ToArray()) as Hub;
                        break;
                    }
                }
            }

            return hub;
        }
    }
}
