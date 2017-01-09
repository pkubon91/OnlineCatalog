using System;
using System.Web;
using System.Web.Security;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Web.Main.Models.Groups;
using OnlineCatalog.Web.Main.Models.UserModel;

namespace OnlineCatalog.Web.Main.Common.Authentication
{
    public class ClientAuthenticationRouter : IRoleAuthenticationRouter
    {
        private static UserRole _userRole = UserRole.Client;

        public IRedirectValues GetRedirectValues(HttpContext httpContext, UserDto user)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (user == null) throw new ArgumentNullException(nameof(user));

            httpContext.User = new CustomPrincipal(new CustomIdentity(user.Login), _userRole);
            if (!Roles.IsUserInRole(_userRole.RoleName)) Roles.AddUserToRole(user.Login, _userRole.RoleName);
            return new RedirectValues("ShopList", "Shop");
        }
    }
}