using KiraNet.GutsMvc.Helper;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace KiraNet.GutsMvc.Implement
{
    public class ControllerCache
    {
        private KiraSpinLock _spinLock = new KiraSpinLock();
        private IDictionary<string, TypeInfo> _cache = new Dictionary<string, TypeInfo>();

        public bool TryGetController(string key, out TypeInfo typeInfo)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                typeInfo = null;
                return false;
            }

            _spinLock.Enter();

            bool result;
            if (_cache.TryGetValue(key.ToLower(), out typeInfo))
            {
                if (typeInfo == null)
                    result = false;
                result = true;
            }
            else
            {
                result = false;
            }

            _spinLock.Exit();

            return result;
        }

        public bool TryAddOrUpdateController(string key, TypeInfo typeInfo)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            if (typeInfo == null)
            {
                return false;
            }


            key = key.ToLower();

            _spinLock.Enter();
            _cache[key] = typeInfo;
            _spinLock.Exit();
            return true;
        }
    }
}
