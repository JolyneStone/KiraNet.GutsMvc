using KiraNet.GutsMVC.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KiraNet.GutsMVC
{
    public class ViewDataDictionary : IDictionary<string, object>
    {
        private readonly Dictionary<string, object> _dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        protected ModelMetadata _modelMetadata;
        protected object _model;

        public ViewDataDictionary() { }

        public ViewDataDictionary(object model)
        {
            _model = model;
        }

        public ViewDataDictionary(ViewDataDictionary viewData)
        {
            foreach (var entry in viewData)
            {
                _dictionary.Add(entry.Key, entry.Value);
            }

            _modelMetadata = viewData.ModelMetadata;
            _model = viewData.Model;
        }

        public object Model
        {
            get { return _model; }
            set
            {
                _modelMetadata = null;
                _model = value;
            }
        }

        public ModelMetadata ModelMetadata
        {
            get
            {
                // 之所以要有元数据，目的是为了不管在Controller还是在View都可以得到Model的元数据
                if (_modelMetadata == null && _model != null)
                {
                    _modelMetadata = ModelMetadataProvider.Current.GetMetadataForType(() => _model, _model.GetType());
                }

                return _modelMetadata;
            }
        }

        public virtual object this[string key] { get => _dictionary[key]; set => _dictionary[key] = value; }

        public ICollection<string> Keys => ((IDictionary<string, object>)_dictionary).Keys;

        public ICollection<object> Values => ((IDictionary<string, object>)_dictionary).Values;

        public int Count => _dictionary.Count;

        public bool IsReadOnly => ((IDictionary<string, object>)_dictionary).IsReadOnly;

        public void Add(string key, object value)
        {
            _dictionary.Add(key, value);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            ((IDictionary<string, object>)_dictionary).Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return ((IDictionary<string, object>)_dictionary).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((IDictionary<string, object>)_dictionary).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return ((IDictionary<string, object>)_dictionary).GetEnumerator();
        }

        public bool Remove(string key)
        {
            return _dictionary.Remove(key);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return ((IDictionary<string, object>)_dictionary).Remove(item);
        }

        public virtual bool TryGetValue(string key, out object value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, object>)_dictionary).GetEnumerator();
        }
    }
}