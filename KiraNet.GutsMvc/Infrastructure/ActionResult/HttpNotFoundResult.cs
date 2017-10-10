using System.Net;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 返回404状态码
    /// </summary>
    public class HttpNotFoundResult : HttpStatusCodeResult
    {
        public HttpNotFoundResult()
        {
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
