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
                var clamis = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "zzq"),
                    new Claim(ClaimTypes.Role, "User")
                };
                var identity = new ClaimsIdentity(clamis, "GutsMvcLogin");

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                //把用户权限添加到User
                httpContext.User = principal;
                Thread.CurrentPrincipal = principal;
            }

            return httpContext.User;
        }
    }
}
