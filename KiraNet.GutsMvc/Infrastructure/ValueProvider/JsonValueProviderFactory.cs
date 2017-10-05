using KiraNet.GutsMVC.Implement;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace KiraNet.GutsMVC.Infrastructure
{
    /// <summary>
    /// 特殊的<see cref="IValueProviderFactory"/>，用于将JSON格式的请求内容转换成字典对象，并据此创建一个DictionaryValueProvider
    /// </summary>
    public sealed class JsonValueProviderFactory : IValueProviderFactory
    {
        public IValueProvider CreateValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            try
            {
                object jsonData = GetDeserializedObject(controllerContext);
                if (jsonData == null)
                {
                    return null;
                }

                Dictionary<string, object> backingStore = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                AddToBackingStore(backingStore, String.Empty, jsonData);
                return new DictionaryValueProvider(backingStore);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将JSON解析成有层次关系的前缀放入字典中，
        /// 这个递归方法很好理解也很牛！！！
        /// </summary>
        /// <param name="backingStore"></param>
        /// <param name="prefix"></param>
        /// <param name="value"></param>
        private static void AddToBackingStore(Dictionary<string,object> backingStore, string prefix, object value)
        {
            if (value is IDictionary<string, object> d)
            {
                foreach (KeyValuePair<string, object> entry in d)
                {
                    // xx.xx.xx
                    AddToBackingStore(backingStore, MakePropertyKey(prefix, entry.Key), entry.Value);
                }
                return;
            }

            if (value is IList l)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    // xx[i]
                    AddToBackingStore(backingStore, MakeArrayKey(prefix, i), l[i]);
                }
                return;
            }

            backingStore.Add(prefix, value);
        }

        private static object GetDeserializedObject(ControllerContext controllerContext)
        {
            if (!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
            {
                // 非JSON请求
                return null;
            }

            StreamReader reader = new StreamReader(controllerContext.HttpContext.Request.RequestStream);
            string bodyText = reader.ReadToEnd();
            if (String.IsNullOrEmpty(bodyText))
            {
                // JSON为空
                return null;
            }

            object jsonData = JsonConvert.DeserializeObject(bodyText);
            return jsonData;
        }

        private static string MakeArrayKey(string prefix, int index)
        {
            // 毋需担心prefix为空，根据json的格式，数组只有"xxx":[ "xxx", "xxx" ] 这种格式
            // 而上面会先被解析成类似Dictionary<string, IList>的对象
            // 对该对象进行枚举时，才会进行数组的匹配
            return prefix + "[" + index.ToString(CultureInfo.CurrentCulture) + "]";
        }

        private static string MakePropertyKey(string prefix, string propertyName)
        {
            return (String.IsNullOrWhiteSpace(prefix)) ? propertyName : prefix + "." + propertyName;
        }
    }
}
