using System;
using System.Collections.Generic;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Web.Main.ShopRepositoryClient;

namespace OnlineCatalog.Web.Main.Common.Authentication
{
    public class RoleAuthenticationRouterFactory : IRoleAuthenticationRouterFactory
    {
        private readonly Dictionary<UserRankDto, IRoleAuthenticationRouter> Implementations;

        public RoleAuthenticationRouterFactory(IShopRepositoryService shopRepositoryClient)
        {
            if(shopRepositoryClient == null) throw new ArgumentNullException(nameof(shopRepositoryClient));
            Implementations = new Dictionary<UserRankDto, IRoleAuthenticationRouter>
            {
                {UserRankDto.Client, new ClientAuthenticationRouter()},
                {UserRankDto.ShopAdministrator, new ShopAdministratorAuthenticationRouter() },
                {UserRankDto.SystemAdministrator, new AdministratorAuthenticationRouter(new ShopRepositoryServiceClient())}
            };
        }
         
        public IRoleAuthenticationRouter CreateRouter(UserRankDto userRank)
        {
            if (Implementations.ContainsKey(userRank)) return Implementations[userRank];
            throw new ArgumentException("userRank");
        }
    }
}