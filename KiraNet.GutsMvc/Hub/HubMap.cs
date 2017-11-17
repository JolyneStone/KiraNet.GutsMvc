using KiraNet.GutsMvc.Helper;
using System;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.WebSocketHub
{
    public class HubMap
    {
        private static readonly IDictionary<string, Type> _hubMap = new Dictionary<string, Type>();
        private static KiraSpinLock _lock = new KiraSpinLock();
        private static HubMap _instance;

        // µ¥ÀýÄ£Ê½
        public static HubMap Map
        {
            get
            {
                if (_instance == null)
                {
                    _lock.Enter();
                    if (_instance == null)
                    {
                        _instance = new HubMap();
                    }
                    _lock.Exit();
                }

                return _instance;
            }
        }

        private HubMap() { }

        public void AddHub<THub>(string url) where THub : Hub
        {
            _hubMap.Add(url, typeof(THub));
        }

        public void AddHub(string url, Type hubType)
        {
            if (String.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (hubType == null)
            {
                throw new ArgumentNullException(nameof(hubType));
            }

            _hubMap.Add(url, hubType);
        }

        internal bool TryGetHub(string url, out Type hubType)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("message", nameof(url));
            }

            var tmpUrl = url.TrimEnd('/');
            foreach(var keyValuePair in _hubMap)
            {
                if (tmpUrl.StartsWith(keyValuePair.Key))
                {
                    hubType = keyValuePair.Value;
                    return true;
                }
            }

            hubType = null;
            return false;
        }
    }
}