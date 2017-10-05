using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace KiraNet.GutsMVC.Route
{
    public class RouteData : IDictionary<string, object>
    {
        private readonly IDictionary<string, object> _data = new Dictionary<string, object>();

        public RouteData(object route)
        {
            if (route == null)
                return;

            var propertiess = route.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var property in propertiess)
            {
                var name = property.Name;
                var value = property.GetValue(route);

                _data.Add(name, value);
            }
        }

        public object this[string key] { get => _data[key]; set => _data[key] = value; }

        public ICollection<string> Keys => _data.Keys;

        public ICollection<object> Values => _data.Values;

        public int Count => _data.Count;

        public bool IsReadOnly => _data.IsReadOnly;

        public void Add(string key, object value)
        {
            _data.Add(key, value);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            _data.Add(item);
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _data.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _data.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            _data.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return _data.Remove(key);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return _data.Remove(item);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _data.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
