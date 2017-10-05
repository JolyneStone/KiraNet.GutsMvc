using System;
using System.Collections.Generic;

namespace KiraNet.GutsMVC.Infrastructure
{
    /// <summary>
    /// 表示一个前缀值的容器，用于规则化前缀点缀窗体，然后存储在一个排序的数组中
    /// </summary>
    public class PrefixContainer
    {
        private readonly ICollection<string> _orginalValues;
        private readonly string[] _sortedValues;

        public PrefixContainer(ICollection<string> values)
        {
            _orginalValues = values ?? throw new ArgumentNullException(nameof(values));

            _sortedValues = _orginalValues.ToArrayWithoutNulls();
            Array.Sort(_sortedValues, System.StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 是否包含指定前缀
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public bool ContainsPrefix(string prefix)
        {
            if (prefix == null)
                throw new ArgumentNullException(nameof(prefix));
            if (prefix.Length == 0)
                return _sortedValues.Length > 0; // 空字符串表示任意的前缀

            PrefixComparer prefixComparer = new PrefixComparer(prefix);
            bool contaionsPrefix = Array.BinarySearch(_sortedValues, prefix, prefixComparer) > -1;

            // 以下代码有点疑问，暂时先注释了先。。。
            if (!contaionsPrefix)
            {
                contaionsPrefix = Array.BinarySearch(_sortedValues, prefix + "[", prefixComparer) > -1;
            }

            return contaionsPrefix;
        }

        /// <summary>
        /// 获取所有包含了指定前缀的key
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public IDictionary<string, string> GetKeysFromPrefix(string prefix)
        {
            if (prefix == null)
                throw new ArgumentNullException(nameof(prefix));

            IDictionary<string, string> result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);


            foreach (var entry in _orginalValues)
            {
                if (entry == null || entry.Length == prefix.Length)
                {
                    // 如果enrty的长度正好等于prefix，则说明entry中没有我们所想要的key，因为它的长度只能装下prefix
                    continue;
                }
                if (prefix.Length == 0)
                {
                    GetKeyFromEmptyPrefix(entry, result);
                }
                else if (entry.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    GetKeyFromNonEmptyPrefix(prefix, entry, result);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取key，在无前缀的情况下
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="results"></param>
        private static void GetKeyFromEmptyPrefix(string entry, IDictionary<string, string> results)
        {
            int dotPosition = entry.IndexOf('.');
            int bracketPosition = entry.IndexOf('[');
            int delimiterPosition = -1;

            if(dotPosition == -1 && bracketPosition != -1)
            {
                delimiterPosition = bracketPosition;
            }
            else
            {
                if(bracketPosition == -1)
                {
                    delimiterPosition = dotPosition;
                }
                else
                {
                    delimiterPosition = Math.Min(dotPosition, bracketPosition);
                }
            }

            string key;
            key = delimiterPosition == -1 ? entry : entry.Substring(0, delimiterPosition);
            results[key] = key; // results之所以为字典可能是为了["xxx"]=xxx操作
        }

        private static void GetKeyFromNonEmptyPrefix(string prefix, string entry, IDictionary<string, string> results)
        {
            string key = null;
            string fullName = null;
            int keyPosition = prefix.Length + 1;

            switch (entry[prefix.Length]) // 在调用该方法前应该保证不会溢出
            {
                case '.':
                    // 尝试查找下一个'.'
                    int dotPosition = entry.IndexOf('.', keyPosition);
                    if (dotPosition == -1)
                    {
                        dotPosition = entry.Length;
                    }

                    // For example：Foo.Bar.Name, prefix = Foo
                    // key为两个'.'之间的字符串， 即Bar
                    key = entry.Substring(keyPosition, dotPosition - keyPosition);
                    // fullName为到第二个'.'之前的值， 即Foo.Bar
                    fullName = entry.Substring(0, dotPosition);
                    break;

                case '[':
                    int bracketPosition = entry.IndexOf(']', keyPosition);
                    if (bracketPosition == -1)
                    {
                        // 格式不正确
                        return;
                    }

                    key = entry.Substring(keyPosition, bracketPosition - keyPosition);
                    fullName = entry.Substring(0, bracketPosition + 1);
                    break;

                default:
                    return;
            }

            if (!results.ContainsKey(key))
            {
                results.Add(key, fullName);
            }
        }

        /// <summary>
        /// 匹配前缀
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="testString"></param>
        /// <returns></returns>
        public static bool IsPrefixMatch(string prefix, string testString)
        {
            if (testString == null)
            {
                return false;
            }

            if (prefix.Length == 0)
            {
                return true;
            }

            if (prefix.Length > testString.Length)
            {
                return false; // testString的长度应该大于等于prefix的长度
            }

            if (!testString.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (testString.Length == prefix.Length)
            {
                return true; // 运行到这里说明testString中包含prefix前缀，且和前缀长度相同，则自然是true了
            }

            // testString.Length > prefix.Length
            switch (testString[prefix.Length])
            {
                case '.':
                case '[':
                    return true; // 已知的定界符

                default:
                    return false; // 未知的定界符
            }
        }

        private class PrefixComparer : IComparer<String>
        {
            private string _prefix;

            public PrefixComparer(string prefix)
            {
                _prefix = prefix;
            }

            public int Compare(string x, string y)
            {
                string testString = Object.ReferenceEquals(x, _prefix) ? y : x;
                if (IsPrefixMatch(_prefix, testString))
                {
                    return 0;
                }

                return System.StringComparer.OrdinalIgnoreCase.Compare(x, y);
            }
        }
    }
}