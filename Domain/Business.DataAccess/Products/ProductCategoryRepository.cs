using System;
using System.Collections.Generic;
using System.Linq;
using Business.NHibernate;
using Business.Products;
using NHibernate.Linq;

namespace Business.DataAccess.Products
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

        public List<ProductCategory> GetProductCategoriesForShop(Guid shopUniqueId)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return session.Query<ProductCategory>().Where(category => category.ProductCategoryShop.UniqueId == shopUniqueId).ToList();
            }
        }

        public ProductCategory GetProductCategoryByName(Guid shopId, string name)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return session.Query<ProductCategory>().SingleOrDefault(cat => cat.ProductCategoryShop.UniqueId == shopId && cat.CategoryName == name);
            }
        }

        public ProductCategory GetProductCategory(Guid productCategoryUniqueId)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return session.Get<ProductCategory>(productCategoryUniqueId);
            }
        }

        public ProductCategory EditProductCategory(Guid productCategoryId, ProductCategory productCategory)
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

        public void RemoveProductCategory(Guid id)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                var productCategory = session.Get<ProductCategory>(id);
                if (productCategory == null) return;

                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(productCategory);
                    transaction.Commit();
                }
            }
        }

        public void UpdateProductCategory(ProductCategory productCategory)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(productCategory);
                    transaction.Commit();
                }
            }
        }
    }
}
