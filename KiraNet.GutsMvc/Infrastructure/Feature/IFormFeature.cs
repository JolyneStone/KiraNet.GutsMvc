using System.Threading.Tasks;

namespace KiraNet.GutsMVC
{
    public interface IFormFeature
    {
        /// <summary>
        /// 指示该请求是否支持Content-Type
        /// </summary>
        bool HasFormContentType { get; }

        /// <summary>
        /// 解析Form格式，如果有的话
        /// </summary>
        IFormCollection Form { get; set; }

        /// <summary>
        /// 解析请求body作为From
        /// </summary>
        /// <returns></returns>
        IFormCollection ReadForm();
    }
}