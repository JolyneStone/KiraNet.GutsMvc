using Newtonsoft.Json;
using System.Text;

namespace KiraNet.GutsMvc
{
    public static class SessionExtensions
    {

        /// <summary>
        /// 设置session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool Set<T>(this Session session, string key, T val)
        {
            if (string.IsNullOrWhiteSpace(key) || val == null)
            {
                return false;
            }

            var strVal = JsonConvert.SerializeObject(val);
            var value = Encoding.UTF8.GetBytes(strVal);
            session.AddOrSet(key, value);
            return true;
        }

        /// <summary>
        /// 获取session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this Session session, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return default;
            }

            var val = session.Get(key) as byte[];
            if (val == null)
            {
                return default;
            }

            var strVal = Encoding.UTF8.GetString(val);
            return JsonConvert.DeserializeObject<T>(strVal);
        }
    }
}
