using System;
using System.Web.Mvc;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Products;
using OnlineCatalog.Web.Main.Common;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.Mappings;
using OnlineCatalog.Web.Main.Models.Products;
using OnlineCatalog.Web.Main.ProductCategoryRepositoryClient;
using OnlineCatalog.Web.Main.ProductCategoryServiceClient;

namespace OnlineCatalog.Web.Main.Controllers.ShopAdministration
{
    [Authorize(Roles = UserRoles.ShopAdministrator)]
    public class ProductCategoriesController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductCategoryRepositoryService _productCategoryRepository;

        public ProductCategoriesController(IProductCategoryService productCategoryService, IProductCategoryRepositoryService productCategoryRepository)
        {
            if (productCategoryService == null) throw new ArgumentNullException(nameof(productCategoryService));
            if (productCategoryRepository == null) throw new ArgumentNullException(nameof(productCategoryRepository));
            _productCategoryService = productCategoryService;
            _productCategoryRepository = productCategoryRepository;
        }

        [HttpGet]
        public ViewResult AddProductCategory()
        {
            return View(new ProductCategoryViewModel());
        }

        [HttpPost]
        public ActionResult AddProductCategory(ProductCategoryViewModel productCategory)
        {
            if(!ModelState.IsValid) return AddProductCategory();
            ProductCategoryDto productCategoryDto = productCategory.Map();

            Guid shopGuid;
            if (!Session.TryGetKey(SessionKeys.ShopAdminSelectedShopGuid, out shopGuid)) return View("ShopNotFound");

            productCategoryDto.ShopGuid = shopGuid;

            ServiceActionResult result = _productCategoryService.AddProductCategory(productCategoryDto);
            if (result.Status != ServiceActionResult.Successfull.Status) return AddProductCategory();

            return RedirectToMainView();
        }
        
        public ActionResult RemoveCategory(Guid productCategoryGuid)
        {
            if(!ModelState.IsValid) return RedirectToMainView();
            ServiceActionResult result = _productCategoryService.RemoveProductCategory(productCategoryGuid);
            return RedirectToMainView();
        }

        public ActionResult ProductAssignments(Guid productCategoryGuid)
        {
            throw new NotImplementedException();
        }

        public ActionResult EditCategory(Guid productCategoryGuid)
        {
            ProductCategoryDto productCategory = _productCategoryRepository.GetProductCategory(productCategoryGuid);
            if (productCategory == null) RedirectToMainView();
            return View(productCategory.Map());
        }

        [HttpPost]
        public ActionResult EditProductCategory(ProductCategoryViewModel productCategory)
        {
            if (!ModelState.IsValid) return EditCategory(productCategory.ProductCategoryGuid);
            ProductCategoryDto productCategoryDto = productCategory.Map();

            Guid shopGuid;
            if (!Session.TryGetKey(SessionKeys.ShopAdminSelectedShopGuid, out shopGuid)) return View("ShopNotFound");
            productCategoryDto.ShopGuid = shopGuid;

            ServiceActionResult actionResult = _productCategoryService.EditProductCategory(productCategoryDto);
            if (actionResult.Status != ActionStatus.Successfull) return EditCategory(productCategory.ProductCategoryGuid);

            return RedirectToMainView();
        }
        
        private RedirectToRouteResult RedirectToMainView()
        {
            return RedirectToAction("AdminShopProductCategories", "ShopAdministrationCore");
        }
    }
}