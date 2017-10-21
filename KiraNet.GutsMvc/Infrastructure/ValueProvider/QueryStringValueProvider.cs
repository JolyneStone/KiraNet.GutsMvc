using KiraNet.GutsMvc.Implement;
using System.Globalization;

namespace KiraNet.GutsMvc.Infrastructure
{
    public sealed class QueryStringValueProvider : NameValueCollectionValueProvider
    {
        public QueryStringValueProvider(ControllerContext context) : base(context.HttpContext.Request.QueryString, CultureInfo.CurrentCulture)
        {
        }
    }
}
