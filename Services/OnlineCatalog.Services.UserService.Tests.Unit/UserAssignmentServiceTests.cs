using System;
using Business.Administration;
using Business.DataAccess.Administration;
using Business.DataAccess.Group;
using Business.Groups;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts;

namespace OnlineCatalog.Services.UserService.Tests.Unit
{
    [TestFixture]
    public class UserAssignmentServiceTests
    {
        [Test]
        public void WhenShopRepositoryIsNullThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UserAssignmentService(shopRepository: null, userRepository: Mock.Of<IUserRepository>()));
        }

        [Test]
        public void WhenUserRepositoryIsNullThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UserAssignmentService(shopRepository: Mock.Of<IShopRepository>(), userRepository: null));
        }

        [TestCase(null)]
        [TestCase("")]
        public void WhenLoginIsNullOrEmptyThenReturnNotSuccessfull(string userLogin)
        {
            var service = new UserAssignmentService(Mock.Of<IShopRepository>(), Mock.Of<IUserRepository>());

            ServiceActionResult result = service.AssignUserToShop(Guid.Empty, Guid.Empty);

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenUserToAssignDoesntExistThenReturnNotSuccessfullAcionResult()
        {
            var service = new UserAssignmentService(Mock.Of<IShopRepository>(), Mock.Of<IUserRepository>());

            ServiceActionResult result = service.AssignUserToShop(Guid.Empty, Guid.Parse("5d3e3599-5e38-44e7-a0d9-84abef706163"));

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenSpecifiedShopDoesntExistThenReturnNotSuccessfullActionResult()
        {
            var service = new UserAssignmentService(Mock.Of<IShopRepository>(), Mock.Of<IUserRepository>(r => r.GetUserByLogin("testLogin") == new User("testLogin", "testPassword", UserRank.Client)));

            ServiceActionResult result = service.AssignUserToShop(Guid.Empty, Guid.Parse("5d3e3599-5e38-44e7-a0d9-84abef706163"));

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenUserAndShopExistThenReturnSuccessActionResult()
        {
            var userToAssign = new User("testLogin", "testPassword", UserRank.Client);
            var shop = new Shop();
            var shopRepository = Mock.Of<IShopRepository>(r => r.GetShopById(Guid.Parse("5d3e3599-5e38-44e7-a0d9-84abef706163")) == shop);
            var service = new UserAssignmentService(shopRepository, Mock.Of<IUserRepository>(r => r.GetUserByLogin("testLogin") == userToAssign));

            ServiceActionResult result = service.AssignUserToShop(Guid.Empty, Guid.Parse("5d3e3599-5e38-44e7-a0d9-84abef706163"));

            result.Status.Should().Be(ActionStatus.Successfull);
            Mock.Get(shopRepository).Verify(r => r.AssignUser(shop, userToAssign), Times.Once);
        }

        [Test]
        public void WhenExceptionHappenedThenReturnWithExceptionActionResult()
        {
            var shopRepository = Mock.Of<IShopRepository>();
            Mock.Get(shopRepository).Setup(r => r.GetShopById(Guid.Parse("5d3e3599-5e38-44e7-a0d9-84abef706163"))).Throws<Exception>();
            var service = new UserAssignmentService(shopRepository, Mock.Of<IUserRepository>(r => r.GetUserByLogin("testLogin") == new User("testLogin", "testPassword", UserRank.Client)));

            ServiceActionResult result = service.AssignUserToShop(Guid.Empty, Guid.Parse("5d3e3599-5e38-44e7-a0d9-84abef706163"));

            result.Status.Should().Be(ActionStatus.WithException);
        }
    }
}
