using System;
using System.ServiceModel;
using OnlineCatalog.Common.DataContracts;

namespace OnlineCatalog.Services.OrderService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IBasketService
    {
        [OperationContract]
        ServiceActionResult AddProductToBasket(Guid basketGuid, Guid productGuid);

        [OperationContract]
        ServiceActionResult RemoveProductFromBasket(Guid basGuid, Guid productGuid);
    }
}
