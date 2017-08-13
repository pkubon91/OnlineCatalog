using System;
using System.Collections.Generic;
using Business.Orders;
using Business.Products;

namespace Business.DataAccess.Orders
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Basket GetBasketByUniqueId(Guid basketGuid);

        void AssignProduct(Guid basketGuid, Product product);

        void UpdateBasket(Basket basket);

        Basket GetBasketForShopAndOwner(Guid shopGuid, Guid userGuid, BasketState state);

        IEnumerable<Basket> GetBaskets(Guid shopGuid, Guid userGuid);
    }
}
