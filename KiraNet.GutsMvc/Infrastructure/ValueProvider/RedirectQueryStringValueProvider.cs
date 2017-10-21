using KiraNet.GutsMvc.Implement;
using System.Globalization;

namespace KiraNet.GutsMvc.Infrastructure
{
    public class RedirectQueryStringValueProvider : NameValueCollectionValueProvider
    {
        public RedirectQueryStringValueProvider(ControllerContext context) : base(context.HttpContext.Request.RedirectQueryString, CultureInfo.CurrentCulture)
        {
        }
    }
}
