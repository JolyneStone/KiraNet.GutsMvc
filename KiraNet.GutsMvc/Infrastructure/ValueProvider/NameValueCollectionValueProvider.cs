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

    //public class NameValueCollectionValueProvider :
    //    IEnumerableValueProvider,
    //    IValueProvider
    //{
    //    private PrefixContainer _prefixContainer;
    //    private readonly Dictionary<string, NameValueCollectionValueProviderResultWrapper> _values = new Dictionary<string, NameValueCollectionValueProviderResultWrapper>(StringComparer.OrdinalIgnoreCase);


    //    public NameValueCollectionValueProvider(NameValueCollection collection, CultureInfo culture)
    //    {
    //        if (collection == null)
    //        {
    //            throw new ArgumentNullException("collection");
    //        }

    //        culture = culture ?? CultureInfo.CurrentCulture;

    //        foreach (string key in collection)
    //        {
    //            if (key != null)
    //            {
    //                _values[key] = new NameValueCollectionValueProviderResultWrapper(key, collection, culture);
    //            }
    //        }
    //    }

    //    private PrefixContainer PrefixContainer
    //    {
    //        get
    //        {
    //            if (_prefixContainer == null)
    //            {
    //                _prefixContainer = new PrefixContainer(_values.Keys);
    //            }
    //            return _prefixContainer;
    //        }
    //    }

    //    /// <summary>
    //    /// 指定是否包含指定前缀
    //    /// </summary>
    //    /// <param name="prefix"></param>
    //    /// <returns></returns>
    //    public virtual bool ContainsPrefix(string prefix)
    //    {
    //        return PrefixContainer.ContainsPrefix(prefix);
    //    }

    //    /// <summary>
    //    /// 获取指定key的ValueProviderResult
    //    /// </summary>
    //    /// <param name="key"></param>
    //    /// <returns></returns>
    //    public virtual ValueProviderResult GetValue(string key)
    //    {
    //        if (String.IsNullOrWhiteSpace(key))
    //            throw new ArgumentNullException(nameof(key));

    //        if(_values.TryGetValue(key, out var value))
    //        {
    //            return value.ValueProviderResult;
    //        }

    //        return null;
    //    }

    //    /// <summary>
    //    /// 获取所有包含了指定前缀的key
    //    /// </summary>
    //    /// <param name="prefix"></param>
    //    /// <returns></returns>
    //    public virtual IDictionary<string, string> GetKeysFromPrefix(string prefix)
    //    {
    //        return PrefixContainer.GetKeysFromPrefix(prefix);
    //    }
    //}
}