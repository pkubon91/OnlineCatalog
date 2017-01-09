using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Web.Main.Models.Groups;
using OnlineCatalog.Web.Main.Models.UserModel;
using OnlineCatalog.Web.Main.ShopRepositoryClient;

namespace OnlineCatalog.Web.Main.Common.Authentication
{
    public class AdministratorAuthenticationRouter : IRoleAuthenticationRouter
    {
        private static readonly UserRole UserRole = UserRole.Administrator;
        private readonly IShopRepositoryService _shopRepository;

        public AdministratorAuthenticationRouter(IShopRepositoryService shopRepository)
        {
            if (shopRepository == null) throw new ArgumentNullException(nameof(shopRepository));
            _shopRepository = shopRepository;
        }

        public IRedirectValues GetRedirectValues(HttpContext httpContext, UserDto user)
        {
            if(httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if(user == null) throw new ArgumentNullException(nameof(user));

            httpContext.User = new CustomPrincipal(new CustomIdentity(user.Login), UserRole);
            if (!Roles.IsUserInRole(UserRole.RoleName)) Roles.AddUserToRole(user.Login, UserRole.RoleName);
            return new RedirectValues("AdminShopList", "Shop");
        }
    }
}