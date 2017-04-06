using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Business.DataAccess.Products;
using Business.Products;
using Castle.Core.Internal;
using OnlineCatalog.Common.DataContracts.Mappings.ProductMappings;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Services.ProductCategoryService
{
    public class ProductCategoryRepositoryService : IProductCategoryRepositoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryRepositoryService(IProductCategoryRepository productCategoryRepository)
        {
            if (productCategoryRepository == null) throw new ArgumentNullException(nameof(productCategoryRepository));
            _productCategoryRepository = productCategoryRepository;
        }

        public IEnumerable<ProductCategoryDto> GetProductCategories(Guid shopGuid)
        {
            try
            {
                var productCategories = _productCategoryRepository.GetProductCategoriesForShop(shopGuid);
                if (productCategories.IsNullOrEmpty()) return Enumerable.Empty<ProductCategoryDto>();
                return productCategories.Select(c => c.Map());
            }
            catch (Exception)
            {
                throw new FaultException(new FaultReason("Unexpected error during getting info from DB"));
            }
        }

        public ProductCategoryDto GetProductCategory(Guid productCategoryGuid)
        {
            try
            {
                ProductCategory productCategory = _productCategoryRepository.GetProductCategory(productCategoryGuid);
                return productCategory?.Map();
            }
            catch (Exception)
            {
                throw new FaultException(new FaultReason("Unexcpected error during getting product category from db"));
                throw;
            }
        }
    }
}