namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 对HttpContext的缓存
    /// </summary>
    public interface IHttpContextCache
    {
        HttpContext HttpContext { get; set; }
    }
}
