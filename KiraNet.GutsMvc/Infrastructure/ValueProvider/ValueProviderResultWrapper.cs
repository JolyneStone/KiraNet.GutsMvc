using System;
using System.Globalization;

namespace KiraNet.GutsMVC.Infrastructure
{
    /// <summary>
    /// 用于封装ValueProviderResult
    /// </summary>
    public abstract class ValueProviderResultWrapper<TCollection> : IValueProviderResultWrapper
        where TCollection : class
    {
        protected TCollection _collection;
        protected ValueProviderResult _valueProviderResult;
        protected CultureInfo _culture;
        protected string _key;

        public ValueProviderResultWrapper(string key, TCollection collection, CultureInfo culture)
        {
            if (String.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            _key = key;
            _culture = culture ?? CultureInfo.CurrentCulture;
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public ValueProviderResult ValueProviderResult
        {
            get
            {
                if (_valueProviderResult == null)
                {
                    _valueProviderResult = GetResultFromCollection(_key, _collection, _culture);
                }

                return _valueProviderResult;
            }
        }

        protected abstract ValueProviderResult GetResultFromCollection(string key, TCollection collection, CultureInfo culture);
    }
}
