using System;
using System.Collections.Generic;
using Business.DataAccess.Group;
using Business.Groups;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts.Groups;

namespace OnlineCatalog.Services.ShopService.Tests.Unit
{
    [TestFixture]
    public class ShopRepositoryServiceTests
    {
        [Test]
        public void WhenShopRepositoryIsNullThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ShopRepositoryService(null));
        }

        [TestCase("")]
        [TestCase(null)]
        public void WhenShopNameIsNullOrEmptyThenThrowArgumentNullException(string shopName)
        {
            var shopRepositoryService = new ShopRepositoryService(Mock.Of<IShopRepository>());

            Assert.Throws<ArgumentNullException>(() => shopRepositoryService.GetShopByName(shopName));
        }

        [Test]
        public void WhenShopByNameDoesntExistThenEmptyShopIsReturned()
        {
            var shopRepository = new Mock<IShopRepository>();
            shopRepository.Setup(r => r.GetShopByName("shopName")).Returns((Shop) null);
            var shopRepositoryService = new ShopRepositoryService(shopRepository.Object);

            ShopDto shop = shopRepositoryService.GetShopByName("shopName");

            shop.ShouldBeEquivalentTo(ShopDto.EmptyShop);
        }

        [Test]
        public void WhenRequestedShopExistThenReturnThatShop()
        {
            var shopRepositoryService = new ShopRepositoryService(Mock.Of<IShopRepository>(r => r.GetShopByName("shopName") == new Shop() {Name = "shopName"}));

            ShopDto shop = shopRepositoryService.GetShopByName("shopName");

            shop.Should().NotBeNull();
            shop.Name.Should().Be("shopName");
        }

        [Test]
        public void WhenShopByUniqueIdIsNullTheReturnEmptyShop()
        {
            var shopRepository = new Mock<IShopRepository>();
            shopRepository.Setup(r => r.GetShopById(It.IsAny<Guid>())).Returns((Shop)null);
            var shopRepositoryService = new ShopRepositoryService(shopRepository.Object);

            ShopDto shop = shopRepositoryService.GetShopByUniqueId(It.IsAny<Guid>());

            shop.ShouldBeEquivalentTo(ShopDto.EmptyShop);
        }

        [Test]
        public void WhenShopWithSpecifiedUniqueIdExistThenReturnThatShop()
        {
            Guid shopGuid = Guid.NewGuid();
            var shopRepositoryService = new ShopRepositoryService(Mock.Of<IShopRepository>(r => r.GetShopById(shopGuid) == new Shop() { UniqueId = shopGuid}));

            ShopDto shop = shopRepositoryService.GetShopByUniqueId(shopGuid);

            shop.Should().NotBeNull();
            shop.ShopGuid.Should().Be(shopGuid);
        }

        [Test]
        public void WhenShopRepositoryReturnNullThenEmptyCollectionIsReturned()
        {
            var shopRepository = new Mock<IShopRepository>();
            shopRepository.Setup(r => r.GetAllShops()).Returns((IEnumerable<Shop>) null);
            var shopService = new ShopRepositoryService(shopRepository.Object);

            IEnumerable<ShopDto> shops = shopService.GetAllShops();

            shops.Should().BeEmpty();
        }

        [Test]
        public void WhenShopRepositoryReturnSomeRecordsThenAllRecordsAreMappedIntoShopDto()
        {
            var shopRepository = new Mock<IShopRepository>();
            shopRepository.Setup(r => r.GetAllShops()).Returns(new List<Shop>() {Mock.Of<Shop>(), Mock.Of<Shop>()});
            var shopService = new ShopRepositoryService(shopRepository.Object);

            IEnumerable<ShopDto> shops = shopService.GetAllShops();

            shops.Should().NotBeEmpty();
            shops.Should().HaveCount(2);
        }
    }
}
