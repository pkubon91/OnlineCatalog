using System;
using System.Collections.Generic;
using Business.Administration;
using Business.Groups;
using FluentAssertions;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.DataContracts.Groups;
using OnlineCatalog.Common.DataContracts.Mappings;

namespace OnlineCatalog.Common.DataContracts.Tests.Unit.Mappings
{
    [TestFixture]
    public class ShopMapperTests
    {
        [Test]
        public void ShopDtoIsMapped()
        {
            var shopGuid = Guid.NewGuid();
            var addressGuid = new Guid();
            ShopDto shopDto = new ShopDto()
            {
                Address = new UserAddressDto()
                {
                    AddressGuid = addressGuid,
                    BuildingNumber = "testBuildingNumber",
                    City = "Krakow",
                    Email = "mail@gmail.com",
                    PhoneNumber = "123123123",
                    Street = "Kapelanka"
                },
                Name = "testShopName",
                ShopGuid = shopGuid
            };

            Shop shop = shopDto.Map();

            shop.UniqueId.Should().Be(shopGuid);
            shop.Name.Should().Be(shopDto.Name);
            shop.Address.UniqueId.Should().Be(addressGuid);
            shop.Address.BuildingNumber.Should().Be(shopDto.Address.BuildingNumber);
            shop.Address.City.Should().Be(shopDto.Address.City);
            shop.Address.Email.Should().Be(shopDto.Address.Email);
            shop.Address.Street.Should().Be(shopDto.Address.Street);
        }

        [Test]
        public void ShopIsMappedToDtoObject()
        {
            var shopGuid = Guid.NewGuid();
            var addressGuid = new Guid();
            Shop shop = new Shop()
            {
                Address = new UserAddress()
                {
                    UniqueId = addressGuid,
                    BuildingNumber = "testBuildingNumber",
                    City = "Krakow",
                    Email = "mail@gmail.com",
                    PhoneNumber = "123123123",
                    Street = "Kapelanka"
                },
                Name = "testShopName",
                UniqueId = shopGuid,
                AssignedUsers = new List<User>()
                {
                    new User("testLogin", "testPassword", UserRank.None)
                }
            };

            ShopDto shopDto = shop.Map();

            shopDto.ShopGuid.Should().Be(shopGuid);
            shopDto.Name.Should().Be(shop.Name);
            shopDto.Address.AddressGuid.Should().Be(addressGuid);
            shopDto.Address.BuildingNumber.Should().Be(shop.Address.BuildingNumber);
            shopDto.Address.City.Should().Be(shop.Address.City);
            shopDto.Address.Email.Should().Be(shop.Address.Email);
            shopDto.Address.Street.Should().Be(shop.Address.Street);
        }
    }
}
