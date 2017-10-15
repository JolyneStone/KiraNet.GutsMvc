using System.Collections;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.ModelValid
{
    public class ModelValidDictionary : IDictionary<string, ModelValid>
    {
        private Dictionary<string, ModelValid> _modelValidDictionary = new Dictionary<string, ModelValid>();

        public bool IsValid()
        {
            if (this.Count == 0)
            {
                return true;
            }

            return false;
        }

        public bool IsValid(string key)
        {
            if (this.Count == 0)
            {
                return true;
            }

            var valid = this[key];
            if (valid == null)
            {
                return false;
            }

            return true;
        }

        public void AddRange(IDictionary<string, ModelValid> dictionary)
        {
            if (dictionary == null)
            {
                return;
            }

            foreach (var x in dictionary)
            {
                _modelValidDictionary.Add(x.Key, x.Value);
            }
        }


        public ModelValid this[string key] { get => ((IDictionary<string, ModelValid>)_modelValidDictionary)[key]; set => ((IDictionary<string, ModelValid>)_modelValidDictionary)[key] = value; }

        public ICollection<string> Keys => ((IDictionary<string, ModelValid>)_modelValidDictionary).Keys;

        public ICollection<ModelValid> Values => ((IDictionary<string, ModelValid>)_modelValidDictionary).Values;

        public int Count => ((IDictionary<string, ModelValid>)_modelValidDictionary).Count;

        public bool IsReadOnly => ((IDictionary<string, ModelValid>)_modelValidDictionary).IsReadOnly;

        public IDictionary<string, ModelValid> Dictionary { get; }

        public void Add(string key, ModelValid value) => ((IDictionary<string, ModelValid>)_modelValidDictionary).Add(key, value);

        public void Add(KeyValuePair<string, ModelValid> item)
        {
            if (item.Key == null || item.Value == null)
            {
                return;
            }

            ((IDictionary<string, ModelValid>)_modelValidDictionary).Add(item);
        }

        public void Clear()
        {
            ((IDictionary<string, ModelValid>)_modelValidDictionary).Clear();
        }

        public bool Contains(KeyValuePair<string, ModelValid> item)
        {
            return ((IDictionary<string, ModelValid>)_modelValidDictionary).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return ((IDictionary<string, ModelValid>)_modelValidDictionary).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, ModelValid>[] array, int arrayIndex)
        {
            ((IDictionary<string, ModelValid>)_modelValidDictionary).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, ModelValid>> GetEnumerator()
        {
            return ((IDictionary<string, ModelValid>)_modelValidDictionary).GetEnumerator();
        }

        public bool Remove(string key)
        {
            return ((IDictionary<string, ModelValid>)_modelValidDictionary).Remove(key);
        }

        public bool Remove(KeyValuePair<string, ModelValid> item)
        {
            return ((IDictionary<string, ModelValid>)_modelValidDictionary).Remove(item);
        }

        public bool TryGetValue(string key, out ModelValid value)
        {
            return ((IDictionary<string, ModelValid>)_modelValidDictionary).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<string, ModelValid>)_modelValidDictionary).GetEnumerator();
        }
    }
}
