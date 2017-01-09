using System;
using Business.DataAccess.Products;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Mappings.ProductMappings;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Services.ProductService
{
    public class ProductAdministrationService : IProductAdministrationService
    {
        private readonly IProductRepository _productRepository;

        public ProductAdministrationService(IProductRepository productRepository)
        {
            if (productRepository == null) throw new ArgumentNullException(nameof(productRepository));
            _productRepository = productRepository;
        }

        public ServiceActionResult AddProduct(ProductDto product)
        {
            if(product == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Product was not sent to the service");
            try
            {
                _productRepository.AddToDatabase(product.Map());
                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.NotSuccessfull, ex);
            }
        }

        public ServiceActionResult RemoveProduct(Guid productGuid)
        {
            throw new NotImplementedException();
        }
    }
}