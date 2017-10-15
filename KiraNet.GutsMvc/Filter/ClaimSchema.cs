using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace KiraNet.GutsMvc.Filter
{
    public class ClaimSchema : IClaimSchema
    {
        public IPrincipal CreateSchema(HttpContext httpContext)
        {
            if(Thread.CurrentPrincipal == null)
            {
                Thread.CurrentPrincipal = new ClaimsPrincipal();
            }

            return Thread.CurrentPrincipal;
        }
    }
}
