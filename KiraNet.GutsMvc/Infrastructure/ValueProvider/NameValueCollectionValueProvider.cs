using System;
using System.Collections.Specialized;
using System.Globalization;

namespace KiraNet.GutsMvc.Infrastructure
{

    public class NameValueCollectionValueProvider : ValueProvider<NameValueCollectionValueProviderResultWrapper>
    {
        public NameValueCollectionValueProvider(NameValueCollection collection, CultureInfo culture) : base(culture)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (string key in collection)
            {
                _values[key] = new NameValueCollectionValueProviderResultWrapper(key, collection, culture);
            }
        }
    }
}