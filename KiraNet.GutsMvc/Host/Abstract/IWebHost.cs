namespace KiraNet.GutsMVC
{
    /// <summary>
    /// WEB宿主，用于创建管道
    /// </summary>
    public interface IWebHost
    {
        /// <summary>
        /// 创建管道并启动WEB框架
        /// </summary>
        void Start();
    }
}
