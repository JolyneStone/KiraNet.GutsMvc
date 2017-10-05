using System.Globalization;

namespace KiraNet.GutsMVC.Infrastructure
{
    public sealed class FormCollectionValueProviderResultWrapper : ValueProviderResultWrapper<IFormCollection>
    {
        public FormCollectionValueProviderResultWrapper(string key, IFormCollection collection, CultureInfo culture) : base(key, collection, culture)
        {
        }

        protected override ValueProviderResult GetResultFromCollection(string key, IFormCollection collection, CultureInfo culture)
        {
            if (collection.TryGetValue(key, out var rawValue))
            {
                return new ValueProviderResult(rawValue, rawValue.ContvertToString(), culture);
            }

            return null;
        }
    }
}
