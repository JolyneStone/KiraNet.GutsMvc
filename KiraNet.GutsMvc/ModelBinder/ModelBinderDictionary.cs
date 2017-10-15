using System;
using System.Collections;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.ModelBinder
{
    public class ModelBinderDictionary : IDictionary<Type, IModelBinder>, IModelBinderProvider
    {
        private readonly Dictionary<Type, IModelBinder> _dictionary = new Dictionary<Type, IModelBinder>();
        private IModelBinder _defaultBinder;
        //private ModelBinderProviderCollection _modelBinderProviders;

        //public ModelBinderDictionary()
        //    : this(ModelBinderProviders.BinderProviders)
        //{
        //}

        //internal ModelBinderDictionary(ModelBinderProviderCollection modelBinderProviders)
        //{
        //    _modelBinderProviders = modelBinderProviders;
        //}

        //public IModelBinder DefaultBinder
        //{
        //    get
        //    {
        //        if (_defaultBinder == null)
        //        {
        //            _defaultBinder = new OrdinaryModelBinder();
        //        }

        //        return _defaultBinder;
        //    }
        //    set
        //    {
        //        _defaultBinder = value;
        //    }
        //}

        public IModelBinder GetBinder(Type modelType)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException(nameof(modelType));
            }

            if (_dictionary.TryGetValue(modelType, out var binder))
            {
                return binder;
            }

            // IModelBinder为OrdinaryModelBinder的键
            // 此处我只能采取硬编码的方式来写了
            _dictionary.TryGetValue(typeof(IModelBinder), out binder);

            return binder;
        }

        public int Count => _dictionary.Count;

        public bool IsReadOnly => ((IDictionary<Type, IModelBinder>)_dictionary).IsReadOnly;

        public ICollection<Type> Keys
        {
            get { return _dictionary.Keys; }
        }

        public ICollection<IModelBinder> Values => _dictionary.Values;

        public IModelBinder this[Type key]
        {
            get
            {
                _dictionary.TryGetValue(key, out IModelBinder binder);
                return binder;
            }
            set => _dictionary[key] = value;
        }

        public void Add(KeyValuePair<Type, IModelBinder> item) => 
            ((IDictionary<Type, IModelBinder>)_dictionary).Add(item);

        public void Add(Type key, IModelBinder value) => 
            _dictionary.Add(key, value);

        public void Clear() => 
            _dictionary.Clear();

        public bool Contains(KeyValuePair<Type, IModelBinder> item) =>
            ((IDictionary<Type, IModelBinder>)_dictionary).Contains(item);

        public bool ContainsKey(Type key) =>
            _dictionary.ContainsKey(key);

        public void CopyTo(KeyValuePair<Type, IModelBinder>[] array, int arrayIndex) => 
            ((IDictionary<Type, IModelBinder>)_dictionary).CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<Type, IModelBinder>> GetEnumerator() =>
            _dictionary.GetEnumerator();

        public bool Remove(KeyValuePair<Type, IModelBinder> item) => 
            ((IDictionary<Type, IModelBinder>)_dictionary).Remove(item);

        public bool Remove(Type key) => _dictionary.Remove(key);

        public bool TryGetValue(Type key, out IModelBinder value) => 
            _dictionary.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() =>
            ((IEnumerable)_dictionary).GetEnumerator();
    }
}
