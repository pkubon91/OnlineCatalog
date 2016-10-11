using System.Web.Mvc;
using OnlineCatalog.Web.Main.Models.UserModel;

namespace OnlineCatalog.Web.Main.Controllers.LoginView
{
    public class LoginViewController : Controller
    {
        public ViewResult Login()
        {
            return View(new LoginViewModel());
        }

        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        
        public ViewResult AuthenticateUser()
        {
            if (ModelState.IsValid)
            {
                return new ViewResult();
            }
            return Login();
        }
    }
}