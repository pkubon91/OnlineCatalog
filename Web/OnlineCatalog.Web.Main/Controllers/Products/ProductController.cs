using System;
using System.Web.Mvc;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.Mappings;
using OnlineCatalog.Web.Main.Models.Products;
using OnlineCatalog.Web.Main.ProductAdministrationClient;

namespace OnlineCatalog.Web.Main.Controllers.Products
{
    [Authorize(Roles = UserRoles.ShopAdministrator)]
    public class ProductController : Controller
    {
        private readonly IProductAdministrationService _productAdministrationService;

        public ProductController(IProductAdministrationService productAdministrationService)
        {
            if (productAdministrationService == null) throw new ArgumentNullException(nameof(productAdministrationService));
            _productAdministrationService = productAdministrationService;
        }

        [HttpPost]
        public ActionResult AddProduct(AddProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return AddProduct();

            ServiceActionResult result = _productAdministrationService.AddProduct(productViewModel.ProductView.Map());
            if(result.Status == ActionStatus.Successfull) return RedirectToAction("AdminShopProduct", "ShopAdministrationCore");
            return AddProduct();
        }

        [HttpPost]
        public ActionResult EditProduct(AddProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return AddProduct();
            
            ServiceActionResult result = _productAdministrationService.EditProduct(productViewModel.ProductView.Map());
            if (result.Status == ActionStatus.Successfull) return RedirectToAction("AdminShopProduct", "ShopAdministrationCore");
            return AddProduct();
        }

        public ActionResult RemoveProduct(Guid productGuid)
        {
            ServiceActionResult result = _productAdministrationService.RemoveProduct(productGuid);
            if (result.Status == ActionStatus.Successfull) return RedirectToAction("AdminShopProduct", "ShopAdministrationCore");

            ViewBag.ErrorMessage = result.Reason;
            return View("NotRemovedProduct");
        }

        private ActionResult AddProduct()
        {
            return RedirectToAction("AddProduct", "ShopAdministrationCore");
        }
    }
}