namespace KiraNet.GutsMvc.Filter
{
    /// <summary>
    /// 授权过滤器接口
    /// </summary>
    public interface IAuthorizationFilter
    {
        void OnAuthorization(AuthorizationContext filterContext);
    }
}
