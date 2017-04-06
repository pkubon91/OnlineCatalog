using System.Web.Mvc;
using System.Web.UI.WebControls;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.Models;

namespace OnlineCatalog.Web.Main.Controllers.Navigation
{
    [Authorize(Roles = UserRoles.ShopAdministrator)]
    public class ShopAdminNavController : Controller
    {
        private static MenuItemRouting[] MenuItems = new[]
        {
            MenuItemRouting.ProductCategories, 
            MenuItemRouting.Product, 
            MenuItemRouting.Order,
            MenuItemRouting.User, 
        };

        [Authorize(Roles = UserRoles.ShopAdministrator)]
        public PartialViewResult ShowMenuItems()
        {
            return PartialView("ShopAdminMenuItem", MenuItems);
        }

        [Authorize(Roles = UserRoles.ShopAdministrator)]
        public ActionResult RouteToAction(string menuItemRouting)
        {
            return RedirectToAction(menuItemRouting, "ShopAdministrationCore");
        }
    }
}