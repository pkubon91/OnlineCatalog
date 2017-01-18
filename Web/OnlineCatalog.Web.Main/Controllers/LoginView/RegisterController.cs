using System;
using System.Web.Mvc;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Web.Main.Models.UserModel;
using OnlineCatalog.Web.Main.RegistrationClient;

namespace OnlineCatalog.Web.Main.Controllers.LoginView
{
    public class RegisterController : Controller
    {
        private readonly IRegisterService _registerClient;

        public RegisterController(IRegisterService registerClient)
        {
            if (registerClient == null) throw new ArgumentNullException(nameof(registerClient));
            _registerClient = registerClient;
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterViewModel registeredUser)
        {
            if (ModelState.IsValid)
            {
                var result = _registerClient.RegisterUser(new UserDto
                {
                    Login = registeredUser.Login,
                    Password = registeredUser.Password,
                    Name = registeredUser.Name,
                    Surname = registeredUser.Surname,
                    UserRank = (UserRankDto)registeredUser.UserRank,
                    Address = new UserAddressDto()
                    {
                        BuildingNumber = registeredUser.Address.BuildingNumber,
                        City = registeredUser.Address.City,
                        Email = registeredUser.Address.Email,
                        Street = registeredUser.Address.Street,
                        PhoneNumber = registeredUser.Address.PhoneNumber
                    }
                });
                if (result.Status == ActionStatus.Successfull)
                {
                    return RedirectToAction("Login", "LoginView");
                }
            }
            return RedirectToAction("Register");
        }
    }
}