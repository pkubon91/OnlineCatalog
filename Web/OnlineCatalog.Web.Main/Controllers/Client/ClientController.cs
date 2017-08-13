using System;
using System.Web.Mvc;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Orders;
using OnlineCatalog.Web.Main.BasketClient;
using OnlineCatalog.Web.Main.BasketRepositoryClient;
using OnlineCatalog.Web.Main.Common;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.OrderClient;

namespace OnlineCatalog.Web.Main.Controllers.Client
{
    [Authorize(Roles = UserRoles.Client)]
    public class ClientController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IBasketRepositoryService _basketRepositoryService;
        private readonly IOrderService _orderService;

        public ClientController(IBasketService basketService, IBasketRepositoryService basketRepositoryService, IOrderService orderService)
        {
            if (basketService == null) throw new ArgumentNullException(nameof(basketService));
            if (basketRepositoryService == null) throw new ArgumentNullException(nameof(basketRepositoryService));
            if (orderService == null) throw new ArgumentNullException(nameof(orderService));
            _basketService = basketService;
            _basketRepositoryService = basketRepositoryService;
            _orderService = orderService;
        }

        public ActionResult SelectShop(Guid shopGuid)
        {
            Session[SessionKeys.ClientSelectedShopGuid] = shopGuid;
            return RedirectToAction("ShopProducts", "ClientCore");
        }
        
        public ActionResult AddToBasket(Guid productGuid)
        {
            var shopGuid = Session[SessionKeys.ClientSelectedShopGuid];
            if (shopGuid == null) return View("~/Views/ClientCore/ShopNotFound.cshtml");

            Guid parsedShopGuid;
            if(!Guid.TryParse(shopGuid.ToString(), out parsedShopGuid)) return View("~/Views/ClientCore/ShopNotFound.cshtml");

            ServiceActionResult result = _basketService.AddProductToBasket(parsedShopGuid, productGuid, User.Identity.Name);
            if (result.Status == ActionStatus.Successfull) return View("ProductAddedToBasket");
            return View("UnexpectedError", result);
        }

        public ActionResult OrderBasket()
        {
            var shopGuid = Session[SessionKeys.ClientSelectedShopGuid];
            if (shopGuid == null) return View("~/Views/ClientCore/ShopNotFound.cshtml");

            Guid parsedShopGuid;
            if (!Guid.TryParse(shopGuid.ToString(), out parsedShopGuid)) return View("~/Views/ClientCore/ShopNotFound.cshtml");

            BasketDto basket = _basketRepositoryService.GetShopBasketForClient(parsedShopGuid, User.Identity.Name);
            if (basket.Equals(BasketDto.EmptyBasket)) return View("BasketNotFound");

            ServiceActionResult result = _orderService.FinalizeOrder(basket.BasketGuid);
            if (result.Status == ActionStatus.Successfull) return View("OrderFinalized");
            return View("UnexpectedError", result);
        }

        public ActionResult RemoveProduct(Guid productGuid)
        {
            var shopGuid = Session[SessionKeys.ClientSelectedShopGuid];
            if (shopGuid == null) return View("~/Views/ClientCore/ShopNotFound.cshtml");

            Guid parsedShopGuid;
            if (!Guid.TryParse(shopGuid.ToString(), out parsedShopGuid)) return View("~/Views/ClientCore/ShopNotFound.cshtml");

            BasketDto basket = _basketRepositoryService.GetShopBasketForClient(parsedShopGuid, User.Identity.Name);
            if(basket.Equals(BasketDto.EmptyBasket)) return View("~/Views/Client/BasketNotFound.cshtml");

            ServiceActionResult result = _basketService.RemoveProductFromBasket(basket.BasketGuid, productGuid);
            if (result.Status == ActionStatus.Successfull) return View("ProductRemovedFromBasket");
            return View("UnexpectedError", result);
        }
    }
}