using System.Collections.Generic;
using System.Collections.Specialized;

namespace KiraNet.GutsMVC
{
    public static class NameValueCollectionExtensions
    {
        public static NameValueCollection Create(this NameValueCollection _collection, IEnumerable<KeyValuePair<string,string>> enumerables)
        {
            var collection = new NameValueCollection();
            foreach(var x in enumerables)
            {
                collection.Add(x.Key, x.Value);
            }

            return collection;
        }
    }
}
