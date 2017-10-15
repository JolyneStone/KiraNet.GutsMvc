using System.Security.Claims;
using System.Security.Principal;

namespace KiraNet.GutsMvc.Filter
{
    public interface IClaimSchema
    {
        IPrincipal CreateSchema(HttpContext httpContext);
    }
}
