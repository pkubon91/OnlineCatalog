using System;
using System.Collections.Generic;
using Business.Products;

namespace Business.DataAccess.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        void RemoveProduct(Product product);
        Product GetProductById(Guid productGuid);
        IEnumerable<Product> GetNotDeletedProducts(Guid shopGuid);
        void UpdateProduct(Product product);
    }
}
