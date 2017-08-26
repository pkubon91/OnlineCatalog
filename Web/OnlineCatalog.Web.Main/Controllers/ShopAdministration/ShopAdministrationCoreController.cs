using System;
using System.Linq;
using System.Web.Mvc;
using Castle.Core.Internal;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Common.DataContracts.Products;
using OnlineCatalog.Web.Main.Common;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.Mappings;
using OnlineCatalog.Web.Main.Models.Groups;
using OnlineCatalog.Web.Main.Models.Products;
using OnlineCatalog.Web.Main.ProductCategoryRepositoryClient;
using OnlineCatalog.Web.Main.ProductRepositoryClient;
using OnlineCatalog.Web.Main.ShopRepositoryClient;

namespace OnlineCatalog.Web.Main.Controllers.ShopAdministration
{
    [Authorize(Roles = UserRoles.ShopAdministrator)]
    public class ShopAdministrationCoreController : Controller
    {
        private readonly IProductCategoryRepositoryService _productCategoryRepository;
        private readonly IProductRepositoryService _productRepositoryService;
        private readonly IShopRepositoryService _shopRepository;

        public ShopAdministrationCoreController(IProductCategoryRepositoryService productCategoryRepository, IProductRepositoryService productRepositoryService, IShopRepositoryService shopRepository)
        {
            if (productCategoryRepository == null) throw new ArgumentNullException(nameof(productCategoryRepository));
            if (productRepositoryService == null) throw new ArgumentNullException(nameof(productRepositoryService));
            if (shopRepository == null) throw new ArgumentNullException(nameof(shopRepository));

            _productCategoryRepository = productCategoryRepository;
            _productRepositoryService = productRepositoryService;
            _shopRepository = shopRepository;
        }

        [HttpGet]
        public ViewResult AdminShopProduct()
        {
            Guid shopGuid;
            if (!Session.TryGetKey(SessionKeys.ShopAdminSelectedShopGuid, out shopGuid)) return View("ShopNotFound");

            ProductDto[] shopProducts = _productRepositoryService.GetShopProducts(shopGuid);
            if (shopProducts.IsNullOrEmpty()) return View(Enumerable.Empty<ProductViewModel>());

            return View(shopProducts.Select(p => p.Map()));
        }

        [HttpGet]
        public ViewResult AdminShopProductCategories()
        {
            Guid shopGuid;
            if (!Session.TryGetKey(SessionKeys.ShopAdminSelectedShopGuid, out shopGuid)) return View("ShopNotFound");

            ProductCategoryDto[] productCategories = _productCategoryRepository.GetProductCategories(shopGuid);
            if (productCategories.IsNullOrEmpty()) return View(Enumerable.Empty<ProductCategoryViewModel>());
            return View(productCategories.Select(c => c.Map()));
        }

        public ActionResult EditProduct(Guid productGuid)
        {
            var product = _productRepositoryService.GetProduct(productGuid);
            if (product == ProductDto.EmptyProduct) return AdminShopProduct();

            Guid shopGuid;
            if (!Session.TryGetKey(SessionKeys.ShopAdminSelectedShopGuid, out shopGuid)) return View("ShopNotFound");

            ProductCategoryDto[] productCategories = _productCategoryRepository.GetProductCategories(shopGuid);
            return View(new AddProductViewModel(productCategories.Select(s => s.Map()))
            {
                ProductView = product.Map()
            });
        }

        public ActionResult RemoveProduct(Guid productGuid)
        {
            ProductDto product = _productRepositoryService.GetProduct(productGuid);
            if (product == ProductDto.EmptyProduct) return AdminShopProduct();

            return View(product.Map());
        }

        public ActionResult AddProduct()
        {
            Guid shopGuid;
            if (!Session.TryGetKey(SessionKeys.ShopAdminSelectedShopGuid, out shopGuid)) return View("ShopNotFound");

            ProductCategoryDto[] shopCategories = _productCategoryRepository.GetProductCategories(shopGuid);
            if (shopCategories == null) return View(new AddProductViewModel(Enumerable.Empty<ProductCategoryViewModel>()));
            
            return View(new AddProductViewModel(shopCategories.Select(c => c.Map()))
            {
                ProductView = new ProductViewModel()
                {
                    ShopGuid = shopGuid,
                    CreatedLogin = User.Identity.Name
                }
            });
        }

        public ActionResult AssignedShops()
        {
            ShopDto[] allAssignedShops = _shopRepository.GetShopsAssignedToUserByLogin(User.Identity.Name);
            if (allAssignedShops.IsNullOrEmpty()) return View(Enumerable.Empty<ShopViewModel>());
            return View(allAssignedShops.Select(s => s.Map()));
        }

        public ActionResult SelectShop(Guid shopGuid)
        {
            Session[SessionKeys.ShopAdminSelectedShopGuid] = shopGuid;
            return RedirectToAction("AdminShopProduct");
        }
    }
}