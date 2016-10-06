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
        public void WhenUserLoginIsNotMatchedThenEmptyUserIsReturnedWithNoAuthenticatedPropertySet()
        {
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(r => r.GetUserByLogin("testLogin")).Returns((User) null);
            var loginService = new LoginService(userRepository.Object);

            UserDto loginUser = loginService.LoginUser("testLogin", "testPassword");

            loginUser.Should().Be(UserDto.EmptyUser);
            loginUser.IsAuthenticated.Should().BeFalse();
        }

        [Test]
        public void WhenUserPasswordIsNotMatchedToReturnedUserThenEmptyUserIsReturned()
        {
            var user = new User("testLogin", "testPassword", UserRank.Client);
            var loginService = new LoginService(Mock.Of<IUserRepository>(r => r.GetUserByLogin("testLogin") == user));

            var userDto = loginService.LoginUser("testLogin", "testPassword2");

            userDto.Should().Be(UserDto.EmptyUser);
            userDto.IsAuthenticated.Should().BeFalse();
        }

        [Test]
        public void WhenUserPasswordAndLoginAreMatchedThenUserDtoIsReturned()
        {
            var user = new User("testLogin", "testPassword", UserRank.Client)
            {
                Name = "xName",
                Surname = "xSurname"
            };
            var loginService = new LoginService(Mock.Of<IUserRepository>(r => r.GetUserByLogin("testLogin") == user));

            var userDto = loginService.LoginUser("testLogin", "testPassword");

            userDto.Login.Should().Be(user.Login);
            userDto.Name.Should().Be(user.Name);
            userDto.Surname.Should().Be(user.Surname);
            userDto.IsAuthenticated.Should().BeTrue();
        }
    }
}
