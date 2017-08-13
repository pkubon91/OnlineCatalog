using System.Web.Mvc;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.Models;

namespace OnlineCatalog.Web.Main.Controllers.Navigation
{
    [Authorize(Roles = UserRoles.Client)]
    public class ClientNavController : Controller
    {
        private static readonly MenuItemRouting[] MenuItems =
        {
            MenuItemRouting.ClientShops,
            MenuItemRouting.ClientShopProducts,
            MenuItemRouting.ClientShopBasket,
            MenuItemRouting.ClientOrders
        };

        public PartialViewResult ShowMenuItems()
        {
            return PartialView("ClientMenuItem", MenuItems);
        }

        public ActionResult RouteToAction(string menuItemRouting)
        {
            return RedirectToAction(menuItemRouting, "ClientCore");
        }
    }
}