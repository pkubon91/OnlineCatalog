using System;
using System.Linq;
using System.Web.Mvc;
using OnlineCatalog.Common.DataContracts.Products;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.Mappings;
using OnlineCatalog.Web.Main.Models.Products;
using OnlineCatalog.Web.Main.ProductCategoryRepositoryClient;

namespace OnlineCatalog.Web.Main.Controllers.Navigation
{
    public class ClientNavController : Controller
    {
        private readonly IProductCategoryRepositoryService _productCategoriesRepositoryService;

        public ClientNavController(IProductCategoryRepositoryService productCategoriesRepositoryService)
        {
            if (productCategoriesRepositoryService == null) throw new ArgumentNullException(nameof(productCategoriesRepositoryService));
            _productCategoriesRepositoryService = productCategoriesRepositoryService;
        }

        [Authorize(Roles = UserRoles.Client)]
        [HttpGet]
        public ViewResult ProductCategories(Guid shopGuid)
        {
            if (shopGuid == Guid.Empty) return View("ClientProductCategories", Enumerable.Empty<ProductCategoryViewModel>());
            ProductCategoryDto[] productCategories = _productCategoriesRepositoryService.GetProductCategories(shopGuid);
            if (productCategories == null) return View("ClientProductCategories", Enumerable.Empty<ProductCategoryViewModel>());

            return View("ClientProductCategories", productCategories.Select(c => c.Map()));
        }
    }
}