using System;
using System.ServiceModel;
using OnlineCatalog.Common.DataContracts;

namespace OnlineCatalog.Services.OrderService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IBasketService
    {
        [OperationContract]
        ServiceActionResult AddProductToBasket(Guid shopGuid, Guid productGuid, string loginUser);

        [OperationContract]
        ServiceActionResult RemoveProductFromBasket(Guid basketGuid, Guid productGuid);
    }
}
