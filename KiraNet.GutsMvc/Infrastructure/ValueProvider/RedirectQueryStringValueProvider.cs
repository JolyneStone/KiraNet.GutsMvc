using KiraNet.GutsMvc.Implement;
using System.Globalization;

namespace KiraNet.GutsMvc.Infrastructure
{
    public class RedirectQueryStringValueProvider : NameValueCollectionValueProvider
    {
        public RedirectQueryStringValueProvider(HttpContext context) : base(context.Request.RedirectQueryString, CultureInfo.CurrentCulture)
        {
        }
    }
}
