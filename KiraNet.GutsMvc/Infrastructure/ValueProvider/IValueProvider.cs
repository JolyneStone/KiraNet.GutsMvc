using System.Collections.Generic;

namespace KiraNet.GutsMVC.Infrastructure
{
    /// <summary>
    /// 旨在为目标Action方法提供输入参数的Model绑定的原始数据来源于当前的HTTP请求。
    /// 具体来说，这些原始数据可能来源于当前请求报文的报头集合或者主体内容，也可能来源于请求的URL。
    /// IValueProvider用来为Model绑定提供原始数据
    /// 该对象是一个对数据容器的封装
    /// </summary>
    public interface IValueProvider
    {
        /// <summary>
        /// 定义集合是否包含指定的前缀
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        bool ContainsPrefix(string prefix);
   
        /// <summary>
        /// 该方法根据指定的Key从呗封装的数据容器中提取对应的数据
        /// 这个Key和容器中对应数据条目的Key可能并非完全一致，后者可能再前者基础上添加相应的前缀
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ValueProviderResult GetValue(string key);


        /// <summary>
        /// 以字典的形式返回数据源容器中所有指定前缀的Key
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        IDictionary<string, string> GetKeysFromPrefix(string prefix);
    }
}