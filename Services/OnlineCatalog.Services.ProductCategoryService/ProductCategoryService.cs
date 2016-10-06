using System;
using Business.DataAccess.Product;
using Business.Products;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Mappings.ProductMappings;
using OnlineCatalog.Common.DataContracts.Products;
using OnlineCatalog.Common.Extensions;

namespace OnlineCatalog.Services.ProductCategoryService
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            if(productCategoryRepository == null) throw new ArgumentNullException(nameof(productCategoryRepository));

            _productCategoryRepository = productCategoryRepository;
        }

        public ServiceActionResult AddProductCategory(ProductCategoryDto productCategory)
        {
            if (productCategory == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Product category cannot be null");
            if (productCategory.CategoryName.IsNullOrEmpty()) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Category name cannot be null or empty");
            if (productCategory.ShopGuid.IsEmptyGuid()) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Shop guid cannot be empty");


            //TODO add getting shop from shop service
            ProductCategory category = _productCategoryRepository.GetProductCategoryByName(productCategory.ShopGuid, productCategory.CategoryName);
            if (category != null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Product category already exist");
            _productCategoryRepository.AddToDatabase(productCategory.Map());

            return ServiceActionResult.Successfull;
        }
    }
}