using System.Collections.Generic;
using System.Globalization;

namespace KiraNet.GutsMvc.Infrastructure
{
    public class DictionaryValueProviderResultWrapper<T> : ValueProviderResultWrapper<IDictionary<string, T>>
    {
        public DictionaryValueProviderResultWrapper(string key, IDictionary<string, T> collection, CultureInfo culture) : base(key, collection, culture)
        {
        }

        protected override ValueProviderResult GetResultFromCollection(string key, IDictionary<string, T> collection, CultureInfo culture)
        {
            if(collection.TryGetValue(key, out var rawValue) && rawValue !=null)
            {
                string attemptedValue = rawValue.ToString();
                return new ValueProviderResult(rawValue, attemptedValue, culture);
            }

            return null;
        }
    }
}
