using KiraNet.GutsMVC.Infrastructure;
using System.Globalization;

namespace KiraNet.GutsMVC.Implement
{
    public sealed class QueryStringValueProvider : NameValueCollectionValueProvider
    {
        public QueryStringValueProvider(ControllerContext context) : base(context.HttpContext.Request.QueryString, CultureInfo.CurrentCulture)
        {
        }
    }
}
