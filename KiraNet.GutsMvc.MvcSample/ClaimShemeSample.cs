using KiraNet.GutsMvc.Filter;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace KiraNet.GutsMvc.MvcSample
{
    public class ClaimShemeSample : IClaimSchema
    {
        public IPrincipal CreateSchema(HttpContext httpContext)
        {
            if (Thread.CurrentPrincipal == null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "ZZQ"),
                    new Claim(ClaimTypes.Role, "User")
                };
                var identity = new ClaimsIdentity(claims, "GutsMvcLogin");

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                Thread.CurrentPrincipal = principal;
            }

            return Thread.CurrentPrincipal;
        }
    }
}
