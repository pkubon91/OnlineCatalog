using System;
using System.ServiceModel;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Orders;

namespace OnlineCatalog.Services.OrderService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IOrderService
    {
        [OperationContract]
        ServiceActionResult FinalizeOrder(BasketDto basketDto);
    }
}