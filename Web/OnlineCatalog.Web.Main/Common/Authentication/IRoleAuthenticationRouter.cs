using System.Web;
using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Web.Main.Common.Authentication
{
    public interface IRoleAuthenticationRouter
    {
        IRedirectValues GetRedirectValues(HttpContext httpContext, UserDto user);
    }
}