using System;
using Business.DataAccess.Orders;
using Business.Orders;

namespace OnlineCatalog.Services.OrderService
{
    public class BasketRepositoryService : IBasketRepositoryService
    {
        private readonly IBasketRepository _basketRepository;

        public BasketRepositoryService(IBasketRepository basketRepository)
        {
            if (basketRepository == null) throw new ArgumentNullException(nameof(basketRepository));
            _basketRepository = basketRepository;
        }

        public Basket GetBasketByUniqueId(Guid uniqueId)
        {
            return _basketRepository.GetBasketByUniqueId(uniqueId);
        }
    }
}
