using System;
using System.Web;
using System.Web.Security;
using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Web.Main.Common.Authentication
{
    public class ClientAuthenticationRouter : IRoleAuthenticationRouter
    {
        private static readonly UserRole UserRole = UserRole.Client;

        public IRedirectValues GetRedirectValues(HttpContext httpContext, UserDto user)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (user == null) throw new ArgumentNullException(nameof(user));

            httpContext.User = new CustomPrincipal(new CustomIdentity(user.Login), UserRole);
            if (!Roles.IsUserInRole(UserRole.RoleName)) Roles.AddUserToRole(user.Login, UserRole.RoleName);
            return new RedirectValues("ShopList", "Shop");
        }
    }
}