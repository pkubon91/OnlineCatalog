using System;
using FluentAssertions;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts.Administration;
using OnlineCatalog.Services.RegistrationService.Validations;

namespace OnlineCatalog.Services.RegistrationService.Tests.Unit.Validations
{
    [TestFixture]
    public class UserRegistrationValidatorTests
    {
        [Test]
        public void WhenUserDtoIsEmptyThenReturnFalse()
        {
            UserRegistrationValidator validator = new UserRegistrationValidator();

            bool result = validator.Validate(null);

            result.Should().BeFalse();
        }

        [Test]
        public void WhenUserLoginIsNotFilledThenReturnFalse()
        {
            UserRegistrationValidator validator = new UserRegistrationValidator();

            bool result = validator.Validate(new UserDto());

            result.Should().BeFalse();
        }

        [Test]
        public void WhenUserPasswordIsNotFilledThenReturnFalse()
        {
            UserRegistrationValidator validator = new UserRegistrationValidator();

            bool result = validator.Validate(new UserDto() { Login = "testLogin" });

            result.Should().BeFalse();
        }

        [Test]
        public void WhenAddressIsNullThenReturnFalse()
        {
            UserRegistrationValidator validator = new UserRegistrationValidator();

            bool result = validator.Validate(new UserDto() {Login = "TestLogin", Password = "Password", Address = null});

            result.Should().BeFalse();
        }

        [TestCase("")]
        [TestCase(null)]
        public void WhenAddressEmailIsNullOrEmptyThenReturnFalse(string email)
        {
            UserRegistrationValidator validator = new UserRegistrationValidator();

            bool result = validator.Validate(new UserDto() { Login = "Test login", Password = "Password", Address = new UserAddressDto() { Email = email } });

            result.Should().BeFalse();
        }

        public void WhenAllFieldsAreFilledCorrectlyThenRetrunTrue()
        {
            UserRegistrationValidator validator = new UserRegistrationValidator();

            bool result = validator.Validate(new UserDto() { Login = "Test login", Password = "Password", Address = new UserAddressDto() { Email = "test@gmail.com" } });

            result.Should().BeTrue();
        }
    }
}
