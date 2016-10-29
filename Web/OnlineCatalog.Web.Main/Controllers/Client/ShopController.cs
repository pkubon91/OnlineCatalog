using System.Web.Mvc;
using OnlineCatalog.Web.Main.Common;
using OnlineCatalog.Web.Main.LoginClient;

namespace OnlineCatalog.Web.Main.Controllers.Client
{
    [Authorize(Roles = UserRoles.Client)]
    public class ShopController : Controller
    {
        public ActionResult ShopList(ShopDto[] shopDtos)
        {
            return View(shopDtos);
        }
    }
}