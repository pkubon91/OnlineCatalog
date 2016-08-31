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
        public void WhenUserDtoIsEmptyThenNullReferenceExceptionIsThrown()
        {
            UserRegistrationValidator validator = new UserRegistrationValidator();

            Action a = () => validator.Validate(null);

            a.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenUserLoginIsNotFilledThenArgumentNullExceptionIsThrown()
        {
            UserRegistrationValidator validator = new UserRegistrationValidator();

            Action a = () => validator.Validate(new UserDto());

            a.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenUserPasswordIsNotFilledThenArgumentNullExceptionIsThrown()
        {
            UserRegistrationValidator validator = new UserRegistrationValidator();

            Action a = () => validator.Validate(new UserDto() {Login = "testLogin"});

            a.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenAddressIsNullThenArgumentNullExceptionIsThrown()
        {
            UserRegistrationValidator validator = new UserRegistrationValidator();

            Action a = () => validator.Validate(new UserDto() {Login = "TestLogin", Password = "Password", Address = null});

            a.ShouldThrow<ArgumentNullException>();
        }
    }
}
