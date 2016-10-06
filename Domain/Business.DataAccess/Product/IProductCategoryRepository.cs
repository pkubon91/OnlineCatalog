using System;
using System.Collections.Generic;
using Business.Products;

namespace Business.DataAccess.Product
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        IEnumerable<ProductCategory> GetProductCategoriesForShop(Guid shopUniqueId);

        ProductCategory GetProductCategoryByName(Guid shopId, string name);

        ProductCategory EditProductCategory(Guid productCategoryId, ProductCategory productCategory);

        void RemoveProductCategory(Guid id);
    }
}
