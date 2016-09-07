using System;
using Business.Administration;
using Business.DataAccess.Administration;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Services.LoginService.Tests.Unit
{
    [TestFixture]
    public class LoginServiceTests
    {
        [Test]
        public void WhenUserRepositoryIsNullThenThrowArgumentNullExceptino()
        {
            Assert.Throws<ArgumentNullException>(() => new LoginService(null));
        }

        [TestCase("")]
        [TestCase(null)]
        public void WhenLoginIsNullOrEmptyThenThrowArgumentException(string login)
        {
            var loginService = new LoginService(Mock.Of<IUserRepository>());

            Assert.Throws<ArgumentException>(() => loginService.LoginUser(login, "emptstry"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void WhenPasswordHashIsNullOrEmptyThenThrowArgumentException(string password)
        {
            var loginService = new LoginService(Mock.Of<IUserRepository>());

            Assert.Throws<ArgumentException>(() => loginService.LoginUser("emptstry", password));
        }

        [Test]
        public void WhenUserLoginIsNotMatchedThenEmptyUserIsReturned()
        {
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetUserByLogin("testLogin")).Returns((User) null);
            var loginService = new LoginService(userRepository.Object);

            loginService.LoginUser("testLogin", "testPassword").Should().Be(UserDto.EmptyUser);
        }

        [Test]
        public void WhenUserPasswordIsNotMatchedToReturnedUserThenEmptyUserIsReturned()
        {
            var user = new User("testLogin", "testPassword", isAdminUser: false);
            var loginService = new LoginService(Mock.Of<IUserRepository>(r => r.GetUserByLogin("testLogin") == user));

            var userDto = loginService.LoginUser("testLogin", "testPassword2");

            userDto.Should().Be(UserDto.EmptyUser);
        }

        [Test]
        public void WhenUserPasswordAndLoginAreMatchedThenUserDtoIsReturned()
        {
            var user = new User("testLogin", "testPassword", isAdminUser: false)
            {
                Name = "xName",
                Surname = "xSurname"
            };
            var loginService = new LoginService(Mock.Of<IUserRepository>(r => r.GetUserByLogin("testLogin") == user));

            var userDto = loginService.LoginUser("testLogin", "testPassword");

            userDto.Login.Should().Be(user.Login);
            userDto.Name.Should().Be(user.Name);
            userDto.Surname.Should().Be(user.Surname);
        }
    }
}
