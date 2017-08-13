using System;
using System.Collections.Generic;
using System.Linq;
using Business.Administration;
using Business.DataAccess.Administration;
using Business.DataAccess.Orders;
using Business.Orders;
using Castle.Core.Internal;
using OnlineCatalog.Common.DataContracts.Mappings;
using OnlineCatalog.Common.DataContracts.Orders;
using OnlineCatalog.Common.Extensions;

namespace OnlineCatalog.Services.OrderService
{
    public class BasketRepositoryService : IBasketRepositoryService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUserRepository _userRepository;

        public BasketRepositoryService(IBasketRepository basketRepository, IUserRepository userRepository)
        {
            if (basketRepository == null) throw new ArgumentNullException(nameof(basketRepository));
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            _basketRepository = basketRepository;
            _userRepository = userRepository;
        }

        public BasketDto GetBasketByUniqueId(Guid uniqueId)
        {
            throw new NotImplementedException();
        }

        public BasketDto GetShopBasketForClient(Guid shopGuid, string clientLogin)
        {
            if (clientLogin == null) throw new ArgumentNullException(nameof(clientLogin));
            User user = _userRepository.GetUserByLogin(clientLogin);
            if (user == null) return BasketDto.EmptyBasket;

            Basket basket = _basketRepository.GetBasketForShopAndOwner(shopGuid, user.UniqueId, BasketState.Basket);
            if(basket == null) return BasketDto.EmptyBasket;
            return basket.Map();
        }

        public BasketDto[] GetBaskets(Guid shopGUid, string clientLogin, params OrderState[] orderStates)
        {
            if (clientLogin == null) throw new ArgumentNullException(nameof(clientLogin));
            if (orderStates == null) throw new ArgumentNullException(nameof(orderStates));

            User user = _userRepository.GetUserByLogin(clientLogin);
            if (user == null) return Enumerable.Empty<BasketDto>().ToArray();

            IEnumerable<Basket> baskets = _basketRepository.GetBaskets(shopGUid, user.UniqueId);
            if(baskets.IsNullOrEmpty()) return Enumerable.Empty<BasketDto>().ToArray();

            return baskets.Where(b => b.State.IsIn(orderStates.Select(o => (BasketState)(int)o).ToArray())).Select(b => b.Map()).ToArray();
        }
    }
}