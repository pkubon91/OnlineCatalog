using System;
using System.Security.Principal;

namespace OnlineCatalog.Web.Main.Common.Authentication
{
    public class CustomPrincipal : IPrincipal
    {
        private readonly UserRole _userRole;

        public CustomPrincipal(IIdentity identity, UserRole userRole)
        {
            if (identity == null) throw new ArgumentNullException(nameof(identity));
            if (userRole == null) throw new ArgumentNullException(nameof(userRole));
            Identity = identity;
            _userRole = userRole;
        }

        public bool IsInRole(string role)
        {
            return role == _userRole.RoleName;
        }

        public IIdentity Identity { get; }
    }
}