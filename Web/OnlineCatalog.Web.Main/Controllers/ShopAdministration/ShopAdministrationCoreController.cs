using System;
using System.Linq;
using System.Web.Mvc;
using Castle.Core.Internal;
using OnlineCatalog.Common.DataContracts.Products;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.Mappings;
using OnlineCatalog.Web.Main.Models.Products;
using OnlineCatalog.Web.Main.ProductCategoryRepositoryClient;
using OnlineCatalog.Web.Main.ProductRepositoryClient;

namespace OnlineCatalog.Web.Main.Controllers.ShopAdministration
{
    [Authorize(Roles = UserRoles.ShopAdministrator)]
    public class ShopAdministrationCoreController : Controller
    {
        private readonly IProductCategoryRepositoryService _productCategoryRepository;
        private readonly IProductRepositoryService _productRepositoryService;
        public static readonly Guid ShopGuid = Guid.Parse("ffc105ca-e1b1-45eb-9233-a74d00ff563b");

        public ShopAdministrationCoreController(IProductCategoryRepositoryService productCategoryRepository, IProductRepositoryService productRepositoryService)
        {
            if (productCategoryRepository == null) throw new ArgumentNullException(nameof(productCategoryRepository));
            if (productRepositoryService == null) throw new ArgumentNullException(nameof(productRepositoryService));

            _productCategoryRepository = productCategoryRepository;
            _productRepositoryService = productRepositoryService;
        }

        [HttpGet]
        public ViewResult AdminShopProduct()
        {
            ProductDto[] shopProducts = _productRepositoryService.GetShopProducts(ShopGuid);
            if (shopProducts.IsNullOrEmpty()) return View(Enumerable.Empty<ProductViewModel>());

            return View(shopProducts.Select(p => p.Map()));
        }

        [HttpGet]
        public ViewResult AdminShopProductCategories()
        {
            ProductCategoryDto[] productCategories = _productCategoryRepository.GetProductCategories(ShopGuid);
            if (productCategories.IsNullOrEmpty()) return View(Enumerable.Empty<ProductCategoryViewModel>());
            return View(productCategories.Select(c => c.Map()));
        }

        public ActionResult EditProduct(Guid productGuid)
        {
            var product = _productRepositoryService.GetProduct(productGuid);
            if (product == ProductDto.EmptyProduct) return AdminShopProduct();

            var productCategories = _productCategoryRepository.GetProductCategories(ShopGuid);
            return View(new AddProductViewModel(productCategories.Select(s => s.Map()))
            {
                ProductView = product.Map()
            });
        }

        public ActionResult RemoveProduct(Guid productGuid)
        {
            var product = _productRepositoryService.GetProduct(productGuid);
            if (product == ProductDto.EmptyProduct) return AdminShopProduct();

            return View(product.Map());
        }

        public ActionResult AddProduct()
        {
            ProductCategoryDto[] shopCategories = _productCategoryRepository.GetProductCategories(ShopGuid);
            if (shopCategories == null) return View(new AddProductViewModel(Enumerable.Empty<ProductCategoryViewModel>()));
            //TODO Shop guid
            return View(new AddProductViewModel(shopCategories.Select(c => c.Map()))
            {
                ProductView = new ProductViewModel()
                {
                    ShopGuid = ShopGuid,
                    CreatedLogin = User.Identity.Name
                }
            });
        }
    }
}