using System.Net;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 返回401状态码
    /// </summary>
    public class HttpUnauthorizedResult : HttpStatusCodeResult
    {
        public HttpUnauthorizedResult()
        {
            StatusCode = (int)HttpStatusCode.Unauthorized;
            StatusDescription = "Unauthorized";
        }
    }
}
