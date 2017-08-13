using System;
using System.Linq;
using System.Web.Mvc;
using Castle.Core.Internal;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Common.DataContracts.Orders;
using OnlineCatalog.Common.DataContracts.Products;
using OnlineCatalog.Web.Main.BasketRepositoryClient;
using OnlineCatalog.Web.Main.Common;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.Mappings;
using OnlineCatalog.Web.Main.Models.Basket;
using OnlineCatalog.Web.Main.Models.Groups;
using OnlineCatalog.Web.Main.Models.Products;
using OnlineCatalog.Web.Main.ProductRepositoryClient;
using OnlineCatalog.Web.Main.ShopRepositoryClient;

namespace OnlineCatalog.Web.Main.Controllers.Client
{
    [Authorize(Roles = UserRoles.Client)]
    public class ClientCoreController : Controller
    {
        private readonly IShopRepositoryService _shopRepository;
        private readonly IProductRepositoryService _productRepository;
        private readonly IBasketRepositoryService _basketRepository;

        public ClientCoreController(IShopRepositoryService shopRepository, IProductRepositoryService productRepository, IBasketRepositoryService basketRepository)
        {
            if (shopRepository == null) throw new ArgumentNullException(nameof(shopRepository));
            if (productRepository == null) throw new ArgumentNullException(nameof(productRepository));
            if (basketRepository == null) throw new ArgumentNullException(nameof(basketRepository));
            _shopRepository = shopRepository;
            _productRepository = productRepository;
            _basketRepository = basketRepository;
        }

        public ActionResult Shops()
        {
            ShopDto[] shops = _shopRepository.GetAllShops();
            if (shops.IsNullOrEmpty()) return View(Enumerable.Empty<ShopViewModel>());
            return View(shops.Select(shop => shop.Map()));
        }

        public ActionResult ShopProducts()
        {
            if (Session[SessionKeys.ClientSelectedShopGuid] == null) return View("ShopNotFound");
            
            Guid parsedShopGuid;
            if (!Guid.TryParse(Session[SessionKeys.ClientSelectedShopGuid].ToString(), out parsedShopGuid)) return View("ShopNotFound");

            ProductDto[] products = _productRepository.GetShopProducts(parsedShopGuid);
            if (products.IsNullOrEmpty()) return View(Enumerable.Empty<ProductViewModel>());
            return View(products.Select(p => p.Map()));
        }

        public ActionResult ShopBasket()
        {
            if (Session[SessionKeys.ClientSelectedShopGuid] == null) return View("ShopNotFound");

            Guid parsedShopGuid;
            if (!Guid.TryParse(Session[SessionKeys.ClientSelectedShopGuid].ToString(), out parsedShopGuid)) return View("ShopNotFound");

            BasketDto basket = _basketRepository.GetShopBasketForClient(parsedShopGuid, User.Identity.Name);
            if(basket.Equals(BasketDto.EmptyBasket)) return View("BasketProductView", new BasketViewModel());
            if(basket.Products.IsNullOrEmpty()) View("BasketProductView", new BasketViewModel());

            return View("BasketProductView", new BasketViewModel() {Products = basket.Products.Select(p => p.Map())});
        }

        public ActionResult ClientOrders()
        {
            if (Session[SessionKeys.ClientSelectedShopGuid] == null) return View("ShopNotFound");

            Guid parsedShopGuid;
            if (!Guid.TryParse(Session[SessionKeys.ClientSelectedShopGuid].ToString(), out parsedShopGuid)) return View("ShopNotFound");

            BasketDto[] allOrdersForShop = _basketRepository.GetBaskets(parsedShopGuid, User.Identity.Name,
                new[] {OrderState.OrderInProgress, OrderState.OrderSent, OrderState.Ordered});
            return View("ClientOrders", allOrdersForShop.Select(o => o.Map()).OrderBy(o => o.OrderState).ThenByDescending(o => o.UpdateDateTime));
        }
    }
}