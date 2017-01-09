using System;
using Business.Administration;
using Business.DataAccess.Administration;
using Business.DataAccess.Group;
using Business.Groups;
using OnlineCatalog.Common.DataContracts;

namespace OnlineCatalog.Services.UserService
{
    public class UserAssignmentService : IUserAssignmentService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IUserRepository _userRepository;

        public UserAssignmentService(IShopRepository shopRepository, IUserRepository userRepository)
        {
            if (shopRepository == null) throw new ArgumentNullException(nameof(shopRepository));
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            _shopRepository = shopRepository;
            _userRepository = userRepository;
        }

        public ServiceActionResult AssignUserToShop(string login, Guid shopGuid)
        {
            try
            {
                User userToAssign = _userRepository.GetUserByLogin(login);
                if(userToAssign == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, $"User {login} doesn't exist");

                Shop shop = _shopRepository.GetShopById(shopGuid);
                if(shop == null) return new ServiceActionResult(ActionStatus.NotSuccessfull, $"shop with id {shopGuid} doesn't exist");

                _shopRepository.AssignUser(shop, userToAssign);
                return ServiceActionResult.Successfull;
            }
            catch (Exception ex)
            {
                return new ServiceActionResult(ActionStatus.WithException, ex);
            }
        }
    }
}