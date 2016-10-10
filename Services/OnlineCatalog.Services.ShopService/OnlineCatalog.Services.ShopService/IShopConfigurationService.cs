using System.ServiceModel;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.DataContracts.Groups;

namespace OnlineCatalog.Services.ShopService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IShopConfigurationService
    {
        [OperationContract]
        ServiceActionResult AddNewShop(ShopDto shop);
    }
}
