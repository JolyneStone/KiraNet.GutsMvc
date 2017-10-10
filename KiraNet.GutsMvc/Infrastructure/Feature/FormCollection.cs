using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 表示form的值的容器
    /// </summary>
    public class FormCollection : IFormCollection
    {
        public static readonly FormCollection Empty = new FormCollection();
        private static readonly string[] EmptyKeys = Array.Empty<string>();
        private static readonly StringValues[] EmptyValues = Array.Empty<StringValues>();
        private static readonly Enumerator EmptyEnumerator = new Enumerator();
        private static readonly IEnumerator<KeyValuePair<string, StringValues>> EmptyIEnumeratorType = EmptyEnumerator;
        private static readonly IEnumerator EmptyIEnumerator = EmptyEnumerator;
        private static IFormFileCollection EmptyFiles = new FormFileCollection();

        private IFormFileCollection _files;

        private FormCollection() { }

        public FormCollection(Dictionary<string, StringValues> fields, IFormFileCollection files = null)
        {
            _store = fields;
            _files = files;
        }

        public IFormFileCollection Files
        {
            get => _files ?? EmptyFiles;
            private set => _files = value;
        }

        private Dictionary<string, StringValues> _store;

        /// <summary>
        /// 不存在则返回StringValues.Empty
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public StringValues this[string key]
        {
            get
            {
                if (_store == null)
                {
                    return StringValues.Empty;
                }

                if (TryGetValue(key, out StringValues value))
                {
                    return value;
                }
                return StringValues.Empty;
            }
        }

        public int Count
        {
            get
            {
                return _store?.Count ?? 0;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                if (_store == null)
                {
                    return EmptyKeys;
                }
                return _store.Keys;
            }
        }

        public bool ContainsKey(string key)
        {
            if (_store == null)
            {
                return false;
            }
            return _store.ContainsKey(key);
        }

        public bool TryGetValue(string key, out StringValues value)
        {
            if (_store == null)
            {
                value = default(StringValues);
                return false;
            }
            return _store.TryGetValue(key, out value);
        }

        public Enumerator GetEnumerator()
        {
            if (_store == null || _store.Count == 0)
            {
                return EmptyEnumerator;
            }
            return new Enumerator(_store.GetEnumerator());
        }

        IEnumerator<KeyValuePair<string, StringValues>> IEnumerable<KeyValuePair<string, StringValues>>.GetEnumerator()
        {
            if (_store == null || _store.Count == 0)
            {
                return EmptyIEnumeratorType;
            }

            return _store.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_store == null || _store.Count == 0)
            {
                return EmptyIEnumerator;
            }

            return _store.GetEnumerator();
        }

        public struct Enumerator : IEnumerator<KeyValuePair<string, StringValues>>
        {
            private Dictionary<string, StringValues>.Enumerator _dictionaryEnumerator;
            private bool _notEmpty;

            internal Enumerator(Dictionary<string, StringValues>.Enumerator dictionaryEnumerator)
            {
                _dictionaryEnumerator = dictionaryEnumerator;
                _notEmpty = true;
            }

            public bool MoveNext()
            {
                if (_notEmpty)
                {
                    return _dictionaryEnumerator.MoveNext();
                }
                return false;
            }

            public KeyValuePair<string, StringValues> Current
            {
                get
                {
                    if (_notEmpty)
                    {
                        return _dictionaryEnumerator.Current;
                    }

                    return default;
                }
            }

            public void Dispose()
            {
                _dictionaryEnumerator.Dispose();
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            void IEnumerator.Reset()
            {
                if (_notEmpty)
                {
                    ((IEnumerator)_dictionaryEnumerator).Reset();
                }
            }
        }

    }
}