using System;

namespace KiraNet.GutsMvc.WebSocketHub
{
    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = true,
        Inherited = true)]
    public class HubProtocolAttribute:Attribute
    {
        public string SubProtocol { get; }

        public HubProtocolAttribute(string subProtocol)
        {
            if (string.IsNullOrWhiteSpace(subProtocol))
            {
                throw new ArgumentException($"{subProtocol}不可为空或空字符串");
            }

            SubProtocol = subProtocol;
        }
    }
}
