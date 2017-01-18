using System;
using System.ServiceModel;
using Business.Orders;

namespace OnlineCatalog.Services.OrderService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IBasketRepositoryService
    {
        [OperationContract]
        Basket GetBasketByUniqueId(Guid uniqueId);
    }
}
