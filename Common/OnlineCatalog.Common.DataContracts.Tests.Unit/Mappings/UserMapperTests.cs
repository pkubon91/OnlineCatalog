using System;
using System.Collections.Generic;
using System.Linq;
using Business.Administration;
using Business.Groups;
using DataContracts.Mappings;
using FluentAssertions;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Common.DataContracts.Groups;

namespace OnlineCatalog.Common.DataContracts.Tests.Unit.Mappings
{
    [TestFixture]
    public class UserMapperTests
    {
        [Test]
        public void UserDtoIsMappedFromBusinessUserObject()
        {
            User user = new User("testLogin", "testPassword", UserRank.Client)
            {
                Address = new UserAddress
                {
                    BuildingNumber = "123",
                    City = "City",
                },
                Name = "Name1",
                Surname = "Surname",
                UniqueId = Guid.NewGuid()
            };
            user.AssignedShops.Add(new Shop() {Name = "shopName" });

            UserDto userDto = user.Map();

            userDto.UserRank.Should().Be(UserRankDto.Client);
            userDto.Name.Should().Be("Name1");
            userDto.Surname.Should().Be("Surname");
            userDto.Login.Should().Be("testLogin");
            userDto.Password.Should().Be("testPassword");
            userDto.Address.BuildingNumber.Should().Be("123");
            userDto.Address.City.Should().Be("City");
            userDto.AssignedShops.First().Name.Should().Be("shopName");
        }

        [Test]
        public void UserIsMappedFromUserDtoObject()
        {
            UserDto userDto = new UserDto
            {
                Address = new UserAddressDto
                {
                    BuildingNumber = "123",
                    City = "City"
                },
                AssignedShops = new List<ShopDto>()
                {
                    new ShopDto()
                    {
                        Name = "Shop1",
                        Address = new UserAddressDto()
                        {
                            BuildingNumber = "132",
                            City = "CityShop"
                        }
                    },
                    new ShopDto()
                    {
                        Name = "Shop2"
                    }
                },
                Login = "testLogin",
                Name = "Name",
                Surname = "Surname",
                Password = "Password",
                UserRank = UserRankDto.ShopAdministrator
            };

            User user = userDto.Map();

            user.Login.Should().Be("testLogin");
            user.Password.Should().Be("Password");
            user.Surname.Should().Be("Surname");
            user.Address.BuildingNumber.Should().Be("123");
            user.Address.City.Should().Be("City");
            user.Name.Should().Be("Name");
            user.AssignedShops.First().Name.Should().Be("Shop1");
            user.AssignedShops.First().Address.City.Should().Be("CityShop");
            user.AssignedShops.ElementAt(1).Name.Should().Be("Shop2");
        }
    }
}
