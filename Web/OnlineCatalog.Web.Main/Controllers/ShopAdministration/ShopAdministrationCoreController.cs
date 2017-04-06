using System;
using System.Linq;
using System.Web.Mvc;
using Castle.Core.Internal;
using OnlineCatalog.Common.DataContracts.Products;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.Mappings;
using OnlineCatalog.Web.Main.Models.Products;
using OnlineCatalog.Web.Main.ProductCategoryRepositoryClient;
using OnlineCatalog.Web.Main.ShopRepositoryClient;

namespace OnlineCatalog.Web.Main.Controllers.ShopAdministration
{
    [Authorize(Roles = UserRoles.ShopAdministrator)]
    public class ShopAdministrationCoreController : Controller
    {
        private readonly IProductCategoryRepositoryService _productCategoryRepository;
        public static readonly Guid ShopGuid = Guid.Parse("ffc105ca-e1b1-45eb-9233-a74d00ff563b");

        public ShopAdministrationCoreController(IProductCategoryRepositoryService productCategoryRepository)
        {
            if (productCategoryRepository == null) throw new ArgumentNullException(nameof(productCategoryRepository));
            _productCategoryRepository = productCategoryRepository;
        }

        [HttpGet]
        public ViewResult AdminShopProduct()
        {
            return View(Guid.Empty);
        }

        [HttpGet]
        public ViewResult AdminShopProductCategories()
        {
            ProductCategoryDto[] productCategories = _productCategoryRepository.GetProductCategories(ShopGuid);
            if (productCategories.IsNullOrEmpty()) return View(Enumerable.Empty<ProductCategoryViewModel>());
            return View(productCategories.Select(c => c.Map()));
        }
    }
}