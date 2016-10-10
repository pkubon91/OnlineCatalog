using System;
using System.ServiceModel;
using OnlineCatalog.Common.DataContracts.Groups;

namespace OnlineCatalog.Services.ShopService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IShopRepositoryService
    {
        [OperationContract]
        ShopDto GetShopByName(string shopName);

        [OperationContract]
        ShopDto GetShopByUniqueId(Guid uniqueId);
    }
}
