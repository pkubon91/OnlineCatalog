using System;
using System.Collections.Generic;
using System.Linq;
using Business.NHibernate;
using Business.Products;
using NHibernate.Linq;

namespace Business.DataAccess.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISessionProvider _sessionProvider;

        public ProductRepository(ISessionProvider sessionProvider)
        {
            if (sessionProvider == null) throw new ArgumentNullException(nameof(sessionProvider));
            _sessionProvider = sessionProvider;
        }

        public void AddToDatabase(Product entity)
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



        public void RemoveProduct(Product product)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(product);
                    transaction.Commit();
                }
            }
        }

        public Product GetProductById(Guid productGuid)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return session.Get<Product>(productGuid);
            }
        }

        public IEnumerable<Product> GetNotDeletedProducts(Guid shopGuid)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return session.Query<Product>().Where(p => p.AssignedShop.UniqueId == shopGuid && p.IsDeleted == false).ToList();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(product);
                    transaction.Commit();
                }
            }
        }
    }
}
