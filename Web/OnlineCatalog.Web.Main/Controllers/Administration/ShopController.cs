using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.Mappings;
using OnlineCatalog.Web.Main.Models.Groups;
using OnlineCatalog.Web.Main.ShopConfigurationClient;
using OnlineCatalog.Web.Main.ShopRepositoryClient;

namespace OnlineCatalog.Web.Main.Controllers.Administration
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly IShopConfigurationService _shopConfigurationClient;
        private readonly IShopRepositoryService _shopRepositoryClient;

        public ShopController(IShopConfigurationService shopConfigurationClient, IShopRepositoryService shopRepositoryClient)
        {
            if (shopConfigurationClient == null) throw new ArgumentNullException(nameof(shopConfigurationClient));
            if (shopRepositoryClient == null) throw new ArgumentNullException(nameof(shopRepositoryClient));
            _shopConfigurationClient = shopConfigurationClient;
            _shopRepositoryClient = shopRepositoryClient;
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Client)]
        public ActionResult ShopList(IEnumerable<ShopViewModel> shops)
        {
            return View(shops);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Administrator)]
        public ActionResult AdminShopList()
        {
            ShopDto[] shops = _shopRepositoryClient.GetAllShops();
            if (shops == null) return View(Enumerable.Empty<ShopViewModel>());
            return View("AdminShopList", shops.Select(s => s.Map()));
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Administrator)]
        public ActionResult AddShop()
        {
            return View(new ShopViewModel());
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Administrator)]
        public ActionResult AddShop(ShopViewModel shop)
        {
            if (!ModelState.IsValid) return AddShop();
            ServiceActionResult addedValue = _shopConfigurationClient.AddNewShop(shop.Map());
            if (addedValue.Status == ActionStatus.Successfull)
            {
                return AdminShopList();
            }
            ViewBag.ErrorMessage = addedValue.Reason;
            return AddShop();
        }
        
        [Authorize(Roles = UserRoles.Administrator)]
        [HttpGet]
        public ActionResult DeactivateShop(Guid shopGuid)
        {
            return ShopActivation(shopGuid, isShopActive: false);
        }

        [Authorize(Roles = UserRoles.Administrator)]
        [HttpGet]
        public ActionResult ActivateShop(Guid shopGuid)
        {
            return ShopActivation(shopGuid, isShopActive: true);
        }

        private ActionResult ShopActivation(Guid shopGuid, bool isShopActive)
        {
            ServiceActionResult activationResult = _shopConfigurationClient.ShopActivation(shopGuid, isActive: isShopActive);
            if (activationResult.Status != ActionStatus.Successfull)
            {
                ViewBag.Error = activationResult.Reason;
            }

            return RedirectToAction("AdminShopList", "Shop");
        }

        [Authorize(Roles = UserRoles.Administrator)]
        [HttpGet]
        public ViewResult EditShop(Guid shopGuid)
        {
            ShopDto shop = _shopRepositoryClient.GetShopByUniqueId(shopGuid);
            if (shop == null)
            {
//                ViewBag.Error = "Shop does not exist";
//                return RedirectToAction("AdminShopList")
            }
            return View("EditShop", shop.Map());
        }

        [Authorize(Roles = UserRoles.Administrator)]
        [HttpPost]
        public ActionResult EditShop(ShopViewModel shop)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = $"Shop {shop.ShopName} cannot be edited";
                return RedirectToAction("AdminShopList");
            }
            ShopDto shopDto = shop.Map();
            ServiceActionResult editResult = _shopConfigurationClient.EditShop(shopDto);
            if (editResult.Status != ActionStatus.Successfull)
            {
                ViewBag.Error = editResult.Reason;
            }
            return RedirectToAction("AdminShopList");
        }
    }
}