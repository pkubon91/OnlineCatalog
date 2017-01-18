using System;
using Business.DataAccess.Orders;
using Business.Orders;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Orders;

namespace OnlineCatalog.Services.OrderService
{
    public class OrdersService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;

        public OrdersService(IBasketRepository basketRepository)
        {
            if (basketRepository == null) throw new ArgumentNullException(nameof(basketRepository));
            _basketRepository = basketRepository;
        }

        public ServiceActionResult FinalizeOrder(BasketDto basketDto)
        {
            if(basketDto == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Basket cannot be null");
            try
            {
                Basket basketToFinalize = _basketRepository.GetBasketByUniqueId(basketDto.BasketGuid);
                if(basketToFinalize == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Basket doesn't exist");
                if(basketToFinalize.IsRealized) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Basket is already finalized");

                basketToFinalize.IsRealized = true;
                _basketRepository.UpdateBasket(basketToFinalize);
                
                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }
    }
}