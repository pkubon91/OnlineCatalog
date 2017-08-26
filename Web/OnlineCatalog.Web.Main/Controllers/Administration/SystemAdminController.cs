using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Core.Internal;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Web.Main.Common.Authentication;
using OnlineCatalog.Web.Main.Mappings;
using OnlineCatalog.Web.Main.Models.Groups;
using OnlineCatalog.Web.Main.Models.UserModel;
using OnlineCatalog.Web.Main.ShopRepositoryClient;
using OnlineCatalog.Web.Main.UserAssignmentClient;
using OnlineCatalog.Web.Main.UserRepositoryClient;

namespace OnlineCatalog.Web.Main.Controllers.Administration
{
    [Authorize(Roles = UserRoles.Administrator)]
    public class SystemAdminController : Controller
    {
        private readonly IUserRepositoryService _userRepository;
        private readonly IShopRepositoryService _shopRepository;
        private readonly IUserAssignmentService _userAssignmentService;

        public SystemAdminController(IUserRepositoryService userRepository, IShopRepositoryService shopRepository, IUserAssignmentService userAssignmentService)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            if (shopRepository == null) throw new ArgumentNullException(nameof(shopRepository));
            if (userAssignmentService == null) throw new ArgumentNullException(nameof(userAssignmentService));

            _userRepository = userRepository;
            _shopRepository = shopRepository;
            _userAssignmentService = userAssignmentService;
        }

        public ActionResult UserManagment()
        {
            UserDto[] allUsers = _userRepository.GetAllUsers(new[] {UserRankDto.Client, UserRankDto.ShopAdministrator});
            if (allUsers.IsNullOrEmpty()) return View(Enumerable.Empty<UserViewModel>());
            return View(allUsers.Select(u => u.Map()));
        }

        public ActionResult AssignShop(Guid userGuid)
        {
            ShopDto[] allShops = _shopRepository.GetAllShops();
            if (allShops.IsNullOrEmpty()) return View(Enumerable.Empty<ShopUserViewModel>());
            ShopDto[] shopsAssignedToUser = _shopRepository.GetShopsAssignedToUser(userGuid);

            return View(allShops.Select(s => new ShopUserViewModel
            {
                IsShopAssigned = shopsAssignedToUser.Contains(s),
                ShopGuid = s.ShopGuid,
                ShopName = s.Name,
                UserIdentity = userGuid
            }));
        }

        public ActionResult AssignShopToUser(Guid userGuid, Guid shopGuid)
        {
            ServiceActionResult result = _userAssignmentService.AssignUserToShop(userGuid, shopGuid);
            if (result.Status != ActionStatus.Successfull) return View("UnexpectedError", result);
            return RedirectToAction("AssignShop", new {userGuid});
        }

        public ActionResult UnassignShopFromUser(Guid userGuid, Guid shopGuid)
        {
            ServiceActionResult result = _userAssignmentService.UnassignUserFromShop(userGuid, shopGuid);
            if (result.Status != ActionStatus.Successfull) return View("UnexpectedError", result);
            return RedirectToAction("AssignShop", new {userGuid});
        }
    }
}