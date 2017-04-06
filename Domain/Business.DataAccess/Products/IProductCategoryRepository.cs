using System;
using System.Collections.Generic;
using Business.Products;

namespace Business.DataAccess.Products
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        List<ProductCategory> GetProductCategoriesForShop(Guid shopUniqueId);

        ProductCategory GetProductCategoryByName(Guid shopId, string name);

        ProductCategory GetProductCategory(Guid productCategoryUniqueId);

        ProductCategory EditProductCategory(Guid productCategoryId, ProductCategory productCategory);

        void RemoveProductCategory(Guid id);

        void UpdateProductCategory(ProductCategory productCategory);
    }
}
