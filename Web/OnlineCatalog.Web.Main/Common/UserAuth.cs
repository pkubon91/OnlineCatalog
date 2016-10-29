using System;
using System.Security.Principal;

namespace OnlineCatalog.Web.Main.Common
{
    public class UserAuth : IPrincipal
    {
        private readonly string _login;
        private readonly string _userRole;
        private readonly Guid _userGuid;

        public UserAuth(string login, string userRole, Guid userGuid)
        {
            if (login == null) throw new ArgumentNullException(nameof(login));
            if (userRole == null) throw new ArgumentNullException(nameof(userRole));
            _login = login;
            _userRole = userRole;
            _userGuid = userGuid;
        }

        public bool IsInRole(string role)
        {
            return role == _userRole;
        }

        public IIdentity Identity => new GenericIdentity(_login);
    }
}