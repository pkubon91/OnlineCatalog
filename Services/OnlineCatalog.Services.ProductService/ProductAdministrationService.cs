using System;
using System.Collections.Generic;
using System.Linq;
using Business.Administration;
using Business.DataAccess.Administration;
using Business.DataAccess.Products;
using Business.Products;
using Castle.Core.Internal;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Mappings.ProductMappings;
using OnlineCatalog.Common.DataContracts.Products;

namespace OnlineCatalog.Services.ProductService
{
    public class ProductAdministrationService : IProductAdministrationService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductAdministrationService(IProductRepository productRepository, IUserRepository userRepository, IProductCategoryRepository productCategoryRepository)
        {
            if (productRepository == null) throw new ArgumentNullException(nameof(productRepository));
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            if (productCategoryRepository == null) throw new ArgumentNullException(nameof(productCategoryRepository));
            _productRepository = productRepository;
            _userRepository = userRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public ServiceActionResult AddProduct(ProductDto product)
        {
            if(product == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Product was not sent to the service");
            try
            {
                Product entity = product.Map();

                User createdBy = _userRepository.GetUserByLogin(product.CreatedBy.Login);
                if(createdBy == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, $"User {product.CreatedBy.Login} does not exists");
                entity.CreatedBy = createdBy;

                IEnumerable<ProductCategory> productCategories = product.ProductCategories.Select(c => _productCategoryRepository.GetProductCategory(c));
                if (!productCategories.IsNullOrEmpty()) entity.Categories = productCategories;

                _productRepository.AddToDatabase(entity);
                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }

        public ServiceActionResult EditProduct(ProductDto product)
        {
            if(product == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Product is not sent to the service");
            try
            {
                Product entity = _productRepository.GetProductById(product.ProductGuid);
                if(entity == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, $"Product with {product.ProductGuid} unique id is not found");

                entity.ProductName = product.ProductName;
                entity.DefaultPrice = product.DefaultPrice;
                entity.IsActive = product.IsActive;
                entity.Description = product.Description;
                entity.Tax = product.Tax;

                IEnumerable<ProductCategory> productCategories = product.ProductCategories.Select(c => _productCategoryRepository.GetProductCategory(c));
                if (!productCategories.IsNullOrEmpty()) entity.Categories = productCategories;

                _productRepository.UpdateProduct(entity);
                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }

        public ServiceActionResult RemoveProduct(Guid productGuid)
        {
            try
            {
                Product product = _productRepository.GetProductById(productGuid);
                if(product == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, "Product does not exists");

                product.IsDeleted = true;
                _productRepository.UpdateProduct(product);
                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }
    }
}