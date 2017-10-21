using KiraNet.GutsMvc.Implement;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public class RedirectToActionResult : IActionResult
    {
        public string ControllerName { get; set; }
        public string ViewName { get; set; }
        public IDictionary<string, object> QueryString { get; set; }
        public HttpContext HttpContext { get; set; }

        public void ExecuteResult(ControllerContext context)
        {
            InitExecute();

            (new MvcMiddlewareHandler().MiddlewareExecute(HttpContext)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task ExecuteResultAsync(ControllerContext context)
        {
            InitExecute();
            await new MvcMiddlewareHandler().MiddlewareExecute(HttpContext);
        }

        private void InitExecute()
        {
            if (String.IsNullOrWhiteSpace(ViewName))
            {
                throw new ArgumentNullException(nameof(ViewName));
            }

            if (String.IsNullOrWhiteSpace(ControllerName))
            {
                throw new ArgumentNullException(nameof(ControllerName));
            }

            HttpContext.Request.RouteEntity.Controller = ControllerName;
            HttpContext.Request.RouteEntity.Action = ViewName;

            HttpContext.Request.RedirectQueryString = new NameValueCollection();
            if (QueryString != null)
            {
                foreach (var query in QueryString)
                {
                    HttpContext.Request.RedirectQueryString.Set(query.Key, query.Value.ToString());
                }
            }
        }
    }
}
