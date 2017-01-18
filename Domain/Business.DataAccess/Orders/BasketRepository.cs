using System;
using Business.NHibernate;
using Business.Orders;

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
            throw new NotImplementedException();
        }

        public Basket GetBasketByUniqueId(Guid basketGuid)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return session.Get<Basket>(basketGuid);
            }
        }

        public void AssignProduct(Basket basket, Business.Products.Product product)
        {
            if(basket == null) throw new ArgumentNullException(nameof(basket));
            if(product == null) throw  new ArgumentNullException(nameof(product));
            using (var session = _sessionProvider.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
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
    }
}
