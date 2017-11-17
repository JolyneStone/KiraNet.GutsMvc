//using KiraNet.GutsMvc.Implement;
//using System.Net;
//using System.Threading.Tasks;

//namespace KiraNet.GutsMvc.Infrastructure.ActionResult
//{
//    public class WebSocketResult : IActionResult
//    {
//        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
//        public void ExecuteResult(ControllerContext context)
//        {
//            new HttpStatusCodeResult()
//            {
//                StatusCode = (int)HttpStatusCode
//            }
//            .ExecuteResult(context);
//        }

//        public Task ExecuteResultAsync(ControllerContext context)
//        {
//            return new HttpStatusCodeResult()
//            {
//                StatusCode = (int)HttpStatusCode
//            }
//            .ExecuteResultAsync(context);
//        }
//    }
//}
