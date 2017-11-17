using KiraNet.GutsMvc.Implement;
using System.Globalization;

namespace KiraNet.GutsMvc.Infrastructure
{
    public sealed class QueryStringValueProvider : NameValueCollectionValueProvider
    {
        public QueryStringValueProvider(HttpContext context) : base(context.Request.QueryString, CultureInfo.CurrentCulture)
        {
        }
    }
}
