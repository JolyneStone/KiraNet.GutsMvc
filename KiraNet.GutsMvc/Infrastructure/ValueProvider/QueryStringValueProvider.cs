using KiraNet.GutsMvc.Infrastructure;
using System.Globalization;

namespace KiraNet.GutsMvc.Implement
{
    public sealed class QueryStringValueProvider : NameValueCollectionValueProvider
    {
        public QueryStringValueProvider(ControllerContext context) : base(context.HttpContext.Request.QueryString, CultureInfo.CurrentCulture)
        {
        }
    }
}
