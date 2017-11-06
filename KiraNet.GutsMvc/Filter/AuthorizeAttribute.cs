using KiraNet.GutsMvc.Filter;
using System;
using System.Linq;
using System.Security.Principal;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 默认授权过滤器
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Method,
        Inherited = true,
        AllowMultiple = false)]
    public class AuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {

        private string[] _roles;
        private string[] _users;

        public string[] Roles
        {
            get => _roles ?? new string[0];
            set => _roles = value ?? new string[0];
        }

        public string[] Users
        {
            get => _users ?? new string[0];
            set => _users = value ?? new string[0];
        }

        public AuthorizeMode AuthorizeMode { get; set; } = AuthorizeMode.And;

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            if (filterContext.ActionDescriptor.Action.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }


            if (!CheckAuthentication(filterContext.HttpContext))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        protected virtual bool CheckAuthentication(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            IPrincipal user = httpContext.User;
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return false;
            }

            if (Users.Length > 0 && !Users.Contains(user.Identity.Name, StringComparer.InvariantCulture))
            {
                return false;
            }

            switch (AuthorizeMode)
            {
                case AuthorizeMode.And:
                    return Roles.Length == 0 ? true : Roles.All(user.IsInRole);
                case AuthorizeMode.Or:
                    return Roles.Length == 0 ? true : Roles.Any(user.IsInRole);
                default:
                    return false;
            }
        }
    }
}
