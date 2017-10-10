using System.Collections.Specialized;
using System.Globalization;

namespace KiraNet.GutsMvc.Infrastructure
{
    public sealed class NameValueCollectionValueProviderResultWrapper : ValueProviderResultWrapper<NameValueCollection>
    {
        public NameValueCollectionValueProviderResultWrapper(string key, NameValueCollection collection, CultureInfo culture) : base(key, collection, culture)
        {
        }

        protected override ValueProviderResult GetResultFromCollection(string key, NameValueCollection collection, CultureInfo culture)
        {
            string[] rawValue = collection.GetValues(key);
            string attemptedValue = collection[key];
            return new ValueProviderResult(rawValue, attemptedValue, culture);
        }
    }
}
