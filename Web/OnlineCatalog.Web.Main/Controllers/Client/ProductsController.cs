using System.Web.Mvc;
using OnlineCatalog.Web.Main.Attributes;
using OnlineCatalog.Web.Main.Common;

namespace OnlineCatalog.Web.Main.Controllers.Client
{
    [AuthorizeUser(Roles = UserRoles.Client)]
    public class ProductsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}