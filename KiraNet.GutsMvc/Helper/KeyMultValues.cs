using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;
using System;

namespace KiraNet.GutsMVC.Helper
{
    public struct KeyMultValuesPair
    {
        private Dictionary<string, StringValues> _dictionary;

        public void Append(string key, string value)
        {
            if (_dictionary == null)
            {
                _dictionary = new Dictionary<string, StringValues>(0, StringComparer.OrdinalIgnoreCase);
            }

            if (_dictionary.TryGetValue(key, out var values))
            {
                if (values.FirstOrDefault(x => x == value) == null)
                {
                    _dictionary[key] = StringValues.Concat(values, value);
                    ValueCount++;
                }
            }
            else
            {
                _dictionary.Add(key, new StringValues(value));
                ValueCount++;
            }
        }

        public void Append(string key, string[] value)
        {
            if (_dictionary == null)
            {
                _dictionary = new Dictionary<string, StringValues>(0, StringComparer.OrdinalIgnoreCase);
            }

            if (_dictionary.TryGetValue(key, out var values))
            {
                var val = value.Concat(values).Distinct<string>().ToArray();
                _dictionary[key] = new StringValues(val);
                ValueCount += val.Length;
            }
            else
            {
                _dictionary.Add(key, new StringValues(value));
                ValueCount += value.Length;
            }
        }

        public int ValueCount { get; private set; }

        public bool HasValues => ValueCount > 0;

        public int KeyCount => _dictionary?.Keys.Count ?? 0;

        public Dictionary<string, StringValues> GetResults()
            => _dictionary ?? new Dictionary<string, StringValues>(0, StringComparer.OrdinalIgnoreCase);
    }
}
