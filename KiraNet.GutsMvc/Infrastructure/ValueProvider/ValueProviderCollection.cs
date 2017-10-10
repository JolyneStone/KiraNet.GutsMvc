using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KiraNet.GutsMvc.Infrastructure
{
    public class ValueProviderCollection : Collection<IValueProvider>, IValueProvider
    {
        public ValueProviderCollection()
        {
        }

        public ValueProviderCollection(IList<IValueProvider> valueProviders):base(valueProviders)
        {
        }


        public bool ContainsPrefix(string prefix)
        {
            if(String.IsNullOrWhiteSpace(prefix))
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            foreach(var valueProvider in this)
            {
                if (valueProvider.ContainsPrefix(prefix))
                    return true;
            }

            return false;
        }

        public IDictionary<string, string> GetKeysFromPrefix(string prefix)
        {
            if (String.IsNullOrWhiteSpace(prefix))
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            foreach(var valueProvider in this)
            {
                var result = valueProvider.GetKeysFromPrefix(prefix);
                if (result != null && result.Any())
                    return result;
            }

            return new Dictionary<string, string>();
        }

        public ValueProviderResult GetValue(string key)
        {
            if(String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            foreach(var valueProvider in this)
            {
                var result = valueProvider.GetValue(key);
                if (result != null)
                    return result;
            }

            return null;
        }
    }
}
