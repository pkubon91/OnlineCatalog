using System;
using Business.DataAccess.Group;
using Business.DataAccess.Products;
using Business.Groups;
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
        private readonly IShopRepository _shopRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IShopRepository shopRepository)
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

            Shop shop = _shopRepository.GetShopById(productCategory.ShopGuid);
            if(shop == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Provided shop doesn't exist");
            ProductCategory category = _productCategoryRepository.GetProductCategoryByName(productCategory.ShopGuid, productCategory.CategoryName);
            if (category != null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Product category already exist");

            ProductCategory entity = productCategory.Map();
            entity.ProductCategoryShop = shop;
            _productCategoryRepository.AddToDatabase(entity);

            return ServiceActionResult.Successfull;
        }

        public ServiceActionResult RemoveProductCategory(Guid productCategoryUniqueId)
        {
            try
            {
                if (productCategoryUniqueId.IsEmptyGuid()) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Guid cannot be empty guid");

                ProductCategory productCategory = _productCategoryRepository.GetProductCategory(productCategoryUniqueId);
                if (productCategory == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, $"Product category with id {productCategoryUniqueId} does not exist");

                _productCategoryRepository.RemoveProductCategory(productCategoryUniqueId);
                return ServiceActionResult.Successfull;
            }
            catch (Exception e)
            {
                return new ServiceActionResult(ActionStatus.WithException, e);
            }
            
        }

        public ServiceActionResult EditProductCategory(ProductCategoryDto productCategory)
        {
            try
            {
                if(productCategory == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Product category cannot be null");
                ProductCategory businessProductCategory = _productCategoryRepository.GetProductCategory(productCategory.ProductCategoryGuid);
                if(businessProductCategory == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, $"Specified product category does not exists {productCategory.ProductCategoryGuid}");
                ProductCategory uniqueNameProductCategory = _productCategoryRepository.GetProductCategoryByName(productCategory.ShopGuid, productCategory.CategoryName);
                if(uniqueNameProductCategory != null) return new ServiceActionResult(ActionStatus.NotSuccessfull, $"The name {productCategory.CategoryName} is not unique");

                businessProductCategory.CategoryName = productCategory.CategoryName;
                _productCategoryRepository.UpdateProductCategory(businessProductCategory);
                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }
    }
}