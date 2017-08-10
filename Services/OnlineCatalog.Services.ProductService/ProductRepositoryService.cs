using System;
using System.Collections.Generic;
using System.Linq;
using Business.DataAccess.Products;
using Business.Products;
using OnlineCatalog.Common.DataContracts.Mappings.ProductMappings;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Services.ProductService
{
    public class ProductRepositoryService : IProductRepositoryService
    {
        private readonly IProductRepository _productRepository;

        public ProductRepositoryService(IProductRepository productRepository)
        {
            if (productRepository == null) throw new ArgumentNullException(nameof(productRepository));
            _productRepository = productRepository;
        }

        public IEnumerable<ProductDto> GetShopProducts(Guid shopGuid)
        {
            IEnumerable<Product> products = _productRepository.GetNotDeletedProducts(shopGuid);
            if (products == null) return Enumerable.Empty<ProductDto>();
            return products.Select(p => p.Map());
        }

        public ProductDto GetProduct(Guid productGuid)
        {
            Product product = _productRepository.GetProductById(productGuid);
            if (product == null) return ProductDto.EmptyProduct;
            return product.Map();
        }
    }
}