using System;
using Business.NHibernate;
using Business.Products;

namespace Business.DataAccess.Product
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ISessionProvider _sessionProvider;

        public ProductCategoryRepository(ISessionProvider sessionProvider)
        {
            if (sessionProvider == null) throw new ArgumentNullException(nameof(sessionProvider));
            _sessionProvider = sessionProvider;
        }

        public void AddToDatabase(ProductCategory entity)
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

        public ProductCategory GetProductCategoryById(int id)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return session.Get<ProductCategory>(id);
            }
        }

        public ProductCategory EditProductCategory(int productCategoryId, ProductCategory productCategory)
        {
            if(productCategory == null) throw new ArgumentNullException(nameof(productCategory));
            
            using (var session = _sessionProvider.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(productCategory);
                    transaction.Commit();
                }
            }
            return productCategory;
        }

        public void RemoveProductCategory(int id)
        {
            var productCategory = GetProductCategoryById(id);
            if (productCategory == null) return;

            using (var session = _sessionProvider.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(productCategory);
                    transaction.Commit();
                }
            }
        }
    }
}
