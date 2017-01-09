using System;

namespace OnlineCatalog.Web.Main.Common.Authentication
{
    public class UserRole
    {
        public static readonly UserRole Client = new UserRole(UserRoles.Client);
        public static readonly UserRole Administrator = new UserRole(UserRoles.Administrator);
        public static readonly UserRole ShopAdministrator = new UserRole(UserRoles.ShopAdministrator);

        private UserRole(string userRoleName)
        {
            if (userRoleName == null) throw new ArgumentNullException(nameof(userRoleName));
            RoleName = userRoleName;
        }

        public string RoleName { get; }
    }
}