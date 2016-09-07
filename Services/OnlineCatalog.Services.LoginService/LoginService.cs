using System;
using Business.DataAccess.Administration;
using DataContracts.Mappings;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.Extensions;

namespace OnlineCatalog.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            _userRepository = userRepository;
        }

        public UserDto LoginUser(string login, string hashPassword)
        {
            if(login.IsNullOrEmpty()) throw new ArgumentException("login cannot be null or empty", nameof(login));
            if(hashPassword.IsNullOrEmpty()) throw new ArgumentException("password cannot be null or empty", nameof(hashPassword));

            var user = _userRepository.GetUserByLogin(login);
            if (user == null) return UserDto.EmptyUser;
            if (user.Password != hashPassword) return UserDto.EmptyUser;

            return user.Map();
        }
    }
}