using System.Web.Mvc;
using OnlineCatalog.Web.Main.Common.Authentication;

namespace OnlineCatalog.Web.Main.Controllers.Products
{
    public class ProductController : Controller
    {
        [Authorize(Roles = UserRoles.ShopAdministrator)]
        public ActionResult ProductList()
        {
            return View();
        }
    }
}