using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace KiraNet.GutsMVC
{
    /// <summary>
    /// 表示HttpRequest的From值的集合
    /// 注：用StringValues可以表示多个值
    /// </summary>
    public interface IFormCollection : IEnumerable<KeyValuePair<string, StringValues>>
    {
        int Count { get; }
        ICollection<string> Keys { get; }
        bool ContainsKey(string key);
        bool TryGetValue(string key, out StringValues value);
        StringValues this[string key] { get; }

        IFormFileCollection Files { get; }
    }
}
