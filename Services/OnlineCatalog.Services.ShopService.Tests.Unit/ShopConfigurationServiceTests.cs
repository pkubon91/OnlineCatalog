using System;
using Business.Administration;
using Business.DataAccess.Group;
using Business.Groups;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.DataContracts.Groups;

namespace OnlineCatalog.Services.ShopService.Tests.Unit
{
    [TestFixture]
    public class ShopConfigurationServiceTests
    {
        [Test]
        public void WhenShopRepositoryIsNullThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ShopConfigurationService(null));
        }

        [Test]
        public void WhenShopDtoIsNullThenReturnNotSuccessfull()
        {
            var shopConfigurationService = new ShopConfigurationService(Mock.Of<IShopRepository>());

            var serviceActionResult = shopConfigurationService.AddNewShop(null);

            serviceActionResult.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenShopAlreadyExistThenReturnNotSuccessfullActionResult()
        {
            var shopConfigurationService = new ShopConfigurationService(Mock.Of<IShopRepository>(r => r.GetShopByName("shopName") == new Shop()));
            var shopDto = new ShopDto() {Name = "shopName"};

            ServiceActionResult serviceActionResult = shopConfigurationService.AddNewShop(shopDto);

            serviceActionResult.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenShopNameDoesntExistThenSuccessfullActionResultIsReturned()
        {
            var repositoryMock = new Mock<IShopRepository>();
            repositoryMock.Setup(r => r.GetShopByName("shopName")).Returns((Shop) null);
            var shopConfigurationService = new ShopConfigurationService(repositoryMock.Object);
            var shopDto = new ShopDto() {Name = "shopName"};

            ServiceActionResult serviceActionResult = shopConfigurationService.AddNewShop(shopDto);

            serviceActionResult.Status.Should().Be(ActionStatus.Successfull);
        }

        [Test]
        public void WhenAddToDatabaseThrowExceptionThenStatusWithExceptionIsReturned()
        {
            var shopRepository = new Mock<IShopRepository>();
            shopRepository.Setup(r => r.GetShopByName(It.IsAny<string>())).Returns((Shop) null);
            shopRepository.Setup(r => r.AddToDatabase(It.IsAny<Shop>())).Throws<Exception>();
            var shopConfigurationService = new ShopConfigurationService(shopRepository.Object);
            var shopDto = new ShopDto() { Name = "shopName" };

            ServiceActionResult serviceActionResult = shopConfigurationService.AddNewShop(shopDto);

            serviceActionResult.Status.Should().Be(ActionStatus.WithException);
        }

        [Test]
        public void WhenShopDoesNotExistsThenReturnNotSuccessfull()
        {
            var shopRepository = new Mock<IShopRepository>();
            shopRepository.Setup(r => r.GetShopById(It.IsAny<Guid>())).Returns((Shop)null);

            var shopConfigurationService = new ShopConfigurationService(shopRepository.Object);

            ServiceActionResult actionResult = shopConfigurationService.ShopActivation(It.IsAny<Guid>(), It.IsAny<bool>());

            actionResult.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenShopExistsThenUpdateShopAndReturnSuccessfull()
        {
            var shopRepository = new Mock<IShopRepository>();
            shopRepository.Setup(r => r.GetShopById(new Guid("{4BA29681-3FA1-431E-8C98-12E3B952BA25}"))).Returns(new Shop());

            var shopConfigurationService = new ShopConfigurationService(shopRepository.Object);

            ServiceActionResult actionResult = shopConfigurationService.ShopActivation(new Guid("{4BA29681-3FA1-431E-8C98-12E3B952BA25}"), It.IsAny<bool>());

            actionResult.Should().Be(ServiceActionResult.Successfull);
        }

        [Test]
        public void WhenExceptionCatchedThenReturnWithExceptionStatus()
        {
            var shopRepository = new Mock<IShopRepository>();
            shopRepository.Setup(r => r.GetShopById(new Guid("{4BA29681-3FA1-431E-8C98-12E3B952BA25}"))).Throws<Exception>();

            var shopConfigurationService = new ShopConfigurationService(shopRepository.Object);

            ServiceActionResult actionResult = shopConfigurationService.ShopActivation(new Guid("{4BA29681-3FA1-431E-8C98-12E3B952BA25}"), It.IsAny<bool>());

            actionResult.Status.Should().Be(ActionStatus.WithException);
        }

        [Test]
        public void WhenShopIsNullThenEditShopReturnsNotSuccessfull()
        {
            var shopConfigurationService = new ShopConfigurationService(Mock.Of<IShopRepository>());

            var result = shopConfigurationService.EditShop(null);

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenShopNotFoundThenEditShopReturnsNotSuccessfull()
        {
            var shopRepository = new Mock<IShopRepository>();
            shopRepository.Setup(r => r.GetShopById(It.IsAny<Guid>())).Returns((Shop)null);

            var shopConfigurationService = new ShopConfigurationService(shopRepository.Object);

            var result = shopConfigurationService.EditShop(new ShopDto());

            result.Status.Should().Be(ActionStatus.NotSuccessfull);
        }

        [Test]
        public void WhenShopFoundThenEditShopReturnSuccessfull()
        {
            var shopRepository = new Mock<IShopRepository>();
            shopRepository.Setup(r => r.GetShopById(new Guid("{4BA29681-3FA1-431E-8C98-12E3B952BA25}"))).Returns(new Shop() {Address = new UserAddress()});
            var shopConfigurationService = new ShopConfigurationService(shopRepository.Object);

            ServiceActionResult actionResult = shopConfigurationService.EditShop(new ShopDto() {ShopGuid = new Guid("{4BA29681-3FA1-431E-8C98-12E3B952BA25}"), Address = new UserAddressDto()});

            actionResult.Should().Be(ServiceActionResult.Successfull);
        }

        [Test]
        public void WhenShopFoundButExceptionOccuredThenEditShopReturnWithExceptionStatus()
        {
            var shopRepository = new Mock<IShopRepository>();
            shopRepository.Setup(r => r.GetShopById(new Guid("{4BA29681-3FA1-431E-8C98-12E3B952BA25}"))).Returns(new Shop());

            var shopConfigurationService = new ShopConfigurationService(shopRepository.Object);

            ServiceActionResult actionResult = shopConfigurationService.EditShop(new ShopDto() { ShopGuid = new Guid("{4BA29681-3FA1-431E-8C98-12E3B952BA25}"), Address = new UserAddressDto() });

            actionResult.Status.Should().Be(ActionStatus.WithException);
        }
    }
}
