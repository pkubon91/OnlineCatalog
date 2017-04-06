using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.Mappings;
using OnlineCatalog.Web.Main.ShopRepositoryClient;

namespace OnlineCatalog.Web.Main.Controllers.Navigation
{
    public class ClientShopNavController : Controller
    {
        private readonly IShopRepositoryService _shopClient;

        public ClientShopNavController(IShopRepositoryService shopClient)
        {
            if (shopClient == null) throw new ArgumentNullException(nameof(shopClient));
            _shopClient = shopClient;
        }

        [Authorize(Roles = UserRoles.Client)]
        [HttpGet]
        public PartialViewResult UserShops()
        {
            IEnumerable<ShopDto> shops = new List<ShopDto>()
            {
                new ShopDto() {Name = "Test1"},
                new ShopDto() {Name = "Test2"},
                new ShopDto() {Name = "Test3"},
                new ShopDto() {Name = "Test4"},
                new ShopDto() {Name = "Test5"}
            };
            return PartialView(shops.Select(s => s.Map()));
        }
    }
}