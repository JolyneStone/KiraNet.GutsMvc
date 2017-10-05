using System;
using System.Collections.Generic;
using System.Globalization;

namespace KiraNet.GutsMVC.Infrastructure
{
    public class DictionaryValueProvider : ValueProvider<DictionaryValueProviderResultWrapper<object>>
    {
        public DictionaryValueProvider(IDictionary<string, object> dictionary):this(dictionary, CultureInfo.CurrentCulture)
        {
        }

        public DictionaryValueProvider(IDictionary<string, object> dictionary, CultureInfo culture) : base(culture)
        {
            if(dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            foreach(string key in dictionary.Keys)
            {
                _values[key] = new DictionaryValueProviderResultWrapper<object>(key, dictionary, culture);
            }
        }
    }
}
