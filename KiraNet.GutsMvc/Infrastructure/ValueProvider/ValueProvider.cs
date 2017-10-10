using System;
using System.Collections.Generic;
using System.Globalization;

namespace KiraNet.GutsMvc.Infrastructure
{
    public abstract class ValueProvider<TWrapper> :
        IValueProvider
        where TWrapper : IValueProviderResultWrapper
    {
        protected PrefixContainer _prefixContainer;
        protected readonly Dictionary<string, TWrapper> _values = new Dictionary<string, TWrapper>(StringComparer.OrdinalIgnoreCase);
        public CultureInfo Culture { get; protected set; }

        protected ValueProvider(CultureInfo culture)
        {
            Culture = culture ?? CultureInfo.CurrentCulture;
        }

        protected virtual PrefixContainer PrefixContainer
        {
            get
            {
                if (_prefixContainer == null)
                {
                    _prefixContainer = new PrefixContainer(_values.Keys);
                }

                return _prefixContainer;
            }
        }

        /// <summary>
        /// 指定是否包含指定前缀
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public virtual bool ContainsPrefix(string prefix)
        {
            return PrefixContainer.ContainsPrefix(prefix);
        }

        /// <summary>
        /// 获取指定key的ValueProviderResult
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual ValueProviderResult GetValue(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (_values.TryGetValue(key, out var value))
            {
                return value.ValueProviderResult;
            }

            return null;
        }

        /// <summary>
        /// 获取所有包含了指定前缀的key
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public virtual IDictionary<string, string> GetKeysFromPrefix(string prefix)
        {
            if (String.IsNullOrWhiteSpace(prefix))
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            return PrefixContainer.GetKeysFromPrefix(prefix);
        }
    }
}
