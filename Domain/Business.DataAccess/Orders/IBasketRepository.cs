using System;
using Business.Orders;

namespace Business.DataAccess.Orders
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Basket GetBasketByUniqueId(Guid basketGuid);

        void AssignProduct(Basket basket, Business.Products.Product product);

        void UpdateBasket(Basket basket);
    }
}
