using System;
using Business.NHibernate;

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

        public void AddToDatabase(Business.Products.Product entity)
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


    }
}
