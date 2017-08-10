using System.Web.Mvc;
using System.Web.Security;

namespace OnlineCatalog.Web.Main.Controllers
{
    public class SharedController : Controller
    {
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "LoginView");
        }
    }
}