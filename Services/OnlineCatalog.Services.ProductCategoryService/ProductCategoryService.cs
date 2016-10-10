using System;
using Business.DataAccess.Product;
using Business.Products;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Mappings;
using OnlineCatalog.Common.DataContracts.Mappings.ProductMappings;
using OnlineCatalog.Common.DataContracts.Products;
using OnlineCatalog.Common.Extensions;
using OnlineCatalog.Services.ProductCategoryService.ShopService;

namespace OnlineCatalog.Services.ProductCategoryService
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IShopRepositoryService _shopRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IShopRepositoryService shopRepository)
        {
            if(productCategoryRepository == null) throw new ArgumentNullException(nameof(productCategoryRepository));
            if (shopRepository == null) throw new ArgumentNullException(nameof(shopRepository));

            _productCategoryRepository = productCategoryRepository;
            _shopRepository = shopRepository;
        }

        public ServiceActionResult AddProductCategory(ProductCategoryDto productCategory)
        {
            if (productCategory == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Product category cannot be null");
            if (productCategory.CategoryName.IsNullOrEmpty()) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Category name cannot be null or empty");
            if (productCategory.ShopGuid.IsEmptyGuid()) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Shop guid cannot be empty");

            var shop = _shopRepository.GetShopByUniqueId(productCategory.ShopGuid);
            if(shop == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Provided shop doesn't exist");
            ProductCategory category = _productCategoryRepository.GetProductCategoryByName(productCategory.ShopGuid, productCategory.CategoryName);
            if (category != null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Product category already exist");

            ProductCategory entity = productCategory.Map();
            entity.ProductCategoryShop = shop.Map();
            _productCategoryRepository.AddToDatabase(entity);

            return ServiceActionResult.Successfull;
        }
    }
}