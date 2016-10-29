using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using OnlineCatalog.Web.Main.LoginClient;
using OnlineCatalog.Web.Main.Models.UserModel;
using OnlineCatalog.Web.Main.ShopRepositoryClient;

namespace OnlineCatalog.Web.Main.Controllers.LoginView
{
    public class LoginViewController : Controller
    {
        private readonly ILoginService _loginClient;

        public LoginViewController(ILoginService loginClient)
        {
            if (loginClient == null) throw new ArgumentNullException(nameof(loginClient));
            _loginClient = loginClient;
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterViewModel()
            {
                //TODO retrieve shop names from shop repository service
                ShopNames = new Dictionary<string, bool>
                {
                    {"abc", false },
                    {"cde", false }
                }
            });
        }
        
        [HttpPost]
        public ActionResult AuthenticateUser(LoginViewModel credentials)
        {
            if (!ModelState.IsValid) return Login();
            var authenticatedUser = _loginClient.LoginUser(credentials.Login, credentials.Password);
            if (authenticatedUser.IsAuthenticated)
            {
                FormsAuthentication.SetAuthCookie(authenticatedUser.Login, true);
                if (authenticatedUser.UserRank == UserRankDto.Client)
                {
                    return RedirectToAction("ShopList", "Shop", authenticatedUser.AssignedShops);
                }
            }
            ViewBag.Error = "Username or password is invalid";
            return RedirectToAction("Login");
        }
    }
}