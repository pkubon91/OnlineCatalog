using System;
using System.Collections.Generic;
using System.Linq;
using Business.Administration;
using Business.DataAccess.Administration;
using Castle.Core.Internal;
using DataContracts.Mappings;
using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Services.UserService
{
    public class UserRepositoryService : IUserRepositoryService
    {
        private readonly IUserRepository _userRepository;

        public UserRepositoryService(IUserRepository userRepository)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            _userRepository = userRepository;
        }

        public UserDto[] GetAllUsers(params UserRankDto[] acceptableUserRanks)
        {
            if (acceptableUserRanks == null) throw new ArgumentNullException(nameof(acceptableUserRanks));

            IEnumerable<User> allUsers = _userRepository.GetUsersAssignedToRank(acceptableUserRanks.Select(r => (UserRank) (int) r).ToArray());
            if (allUsers.IsNullOrEmpty()) return Enumerable.Empty<UserDto>().ToArray();
            return allUsers.Select(u => u.Map()).ToArray();
        }
    }
}