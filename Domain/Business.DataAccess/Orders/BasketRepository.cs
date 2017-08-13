using System;
using System.Collections.Generic;
using Business.NHibernate;
using Business.Orders;
using Business.Products;

namespace Business.DataAccess.Orders
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ISessionProvider _sessionProvider;

        public BasketRepository(ISessionProvider sessionProvider)
        {
            if (sessionProvider == null) throw new ArgumentNullException(nameof(sessionProvider));
            _sessionProvider = sessionProvider;
        }

        public void AddToDatabase(Basket entity)
        {
            if(entity == null) throw new ArgumentNullException(nameof(entity));
            using (var session = _sessionProvider.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        public Basket GetBasketByUniqueId(Guid basketGuid)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return session.Get<Basket>(basketGuid);
            }
        }

        public void AssignProduct(Guid basketGuid, Product product)
        {
            if(product == null) throw  new ArgumentNullException(nameof(product));
            using (var session = _sessionProvider.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var basket = GetBasketByUniqueId(basketGuid);
                    basket.BasketProducts.Add(product);
                    transaction.Commit();
                }
            }
        }

        public void UpdateBasket(Basket basket)
        {
            if(basket == null) throw new ArgumentNullException(nameof(basket));
            using (var session = _sessionProvider.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(basket);
                    transaction.Commit();
                }
            }
        }

        public Basket GetBasketForShopAndOwner(Guid shopGuid, Guid userGuid, BasketState state)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return
                    session.QueryOver<Basket>()
                        .Where(b => b.State == state &&
                                    b.BasketShop.UniqueId == shopGuid &&
                                    b.Owner.UniqueId == userGuid)
                        .Fetch(x => x.BasketProducts).Eager
                        .SingleOrDefault();
            }
        }

        public IEnumerable<Basket> GetBaskets(Guid shopGuid, Guid userGuid)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return
                    session.QueryOver<Basket>()
                        .Where(b => b.BasketShop.UniqueId == shopGuid &&
                                    b.Owner.UniqueId == userGuid)
                        .List<Basket>();
            }
        }
    }
}
