using System;
using System.ServiceModel;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Groups;

namespace OnlineCatalog.Services.ShopService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IShopConfigurationService
    {
        [OperationContract]
        ServiceActionResult AddNewShop(ShopDto shop);

        [OperationContract]
        ServiceActionResult ShopActivation(Guid shopGuid, bool isActive);

        [OperationContract]
        ServiceActionResult EditShop(ShopDto shop);
    }
}
