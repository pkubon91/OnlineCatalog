using System;

namespace Business.DataAccess.Products
{
    public interface IProductRepository : IRepository<Business.Products.Product>
    {
        Business.Products.Product GetProductById(Guid productGuid);
    }
}
