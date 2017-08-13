using System;
using System.ServiceModel;
using OnlineCatalog.Common.DataContracts.Orders;

namespace OnlineCatalog.Services.OrderService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IBasketRepositoryService
    {
        [OperationContract]
        BasketDto GetBasketByUniqueId(Guid uniqueId);

        [OperationContract]
        BasketDto GetShopBasketForClient(Guid shopGuid, string clientLogin);

        [OperationContract]
        BasketDto[] GetBaskets(Guid shopGUid, string clientLogin, params OrderState[] orderStates);
    }
}
