using System;
using System.Security.Cryptography;
using System.Text;

namespace KiraNet.GutsMvc.Helper
{
    internal class CryphtHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string EncryptMD5(string value)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                /// 得到哈希数组
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2")); // 转化为十六进制
                }

                return sb.ToString().ToUpper();
            }
        }

        /// <summary>
        /// HMAC-SHA256加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns>64位字符串</returns>
        public string EncryptSha256(string value)
        {
            return EncryptSha256(value, Encoding.UTF8);
        }

        /// <summary>
        /// HMAC-SHA256加密
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <returns>64位字符串</returns>
        public string EncryptSha256(string value, Encoding encoding)
        {
            var sha256 = new HMACSHA256(encoding.GetBytes(value));
            var bytResult = sha256.ComputeHash(encoding.GetBytes(value));
            var sb = new StringBuilder();

            // 字节类型的数组转换为字符串
            for (int i = 0; i < bytResult.Length; i++)
            {
                sb.Append(String.Format("{0:x}", bytResult[i]).PadLeft(2, '0'));
            }

            return sb.ToString();
        }
    }
}
