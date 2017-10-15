namespace KiraNet.GutsMvc.Filter
{
    /// <summary>
    /// 认证过滤器接口
    /// </summary>
    public interface IAuthenticationFilter
    {
        /// <summary>
        /// 用于对请求实施认证
        /// </summary>
        /// <param name="filterContext"></param>
        void OnAuthentication(AuthenticationContext filterContext);
    }
}
