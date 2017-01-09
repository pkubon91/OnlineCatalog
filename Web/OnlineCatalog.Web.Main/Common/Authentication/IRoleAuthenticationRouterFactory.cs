using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Web.Main.Common.Authentication
{
    public interface IRoleAuthenticationRouterFactory
    {
        IRoleAuthenticationRouter CreateRouter(UserRankDto userRank);
    }
}