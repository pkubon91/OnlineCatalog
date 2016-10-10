using System;
using Business.DataAccess.Group;
using Business.Groups;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts;
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
        public void WhenShopDtoIsNullThenThrowArgumentnullException()
        {
            var shopConfigurationService = new ShopConfigurationService(Mock.Of<IShopRepository>());

            Assert.Throws<ArgumentNullException>(() => shopConfigurationService.AddNewShop(null));
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
    }
}
