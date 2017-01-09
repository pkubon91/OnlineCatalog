using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.LoginClient;
using OnlineCatalog.Web.Main.Models.UserModel;

namespace OnlineCatalog.Web.Main.Controllers.LoginView
{
    public class LoginViewController : Controller
    {
        private readonly ILoginService _loginClient;
        private readonly IRoleAuthenticationRouterFactory _roleAuthenticationRouterFactory;

        public LoginViewController(ILoginService loginClient, IRoleAuthenticationRouterFactory roleAuthenticationRouterFactory)
        {
            if (loginClient == null) throw new ArgumentNullException(nameof(loginClient));
            if (roleAuthenticationRouterFactory == null) throw new ArgumentNullException(nameof(roleAuthenticationRouterFactory));
            _loginClient = loginClient;
            _roleAuthenticationRouterFactory = roleAuthenticationRouterFactory;
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
                IRedirectValues redirectValues = _roleAuthenticationRouterFactory.CreateRouter(authenticatedUser.UserRank)
                        .GetRedirectValues(System.Web.HttpContext.Current, authenticatedUser);

                return RedirectToAction(redirectValues.Action, redirectValues.Controller);
            }
            ViewBag.Error = "Username or password is invalid";
            return RedirectToAction("Login");
        }
    }
}