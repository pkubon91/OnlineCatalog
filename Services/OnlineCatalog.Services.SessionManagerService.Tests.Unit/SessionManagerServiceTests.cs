using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace OnlineCatalog.Services.SessionManagerService.Tests.Unit
{
    [TestFixture]
    public class SessionManagerServiceTests
    {
        [Test]
        public void WhenActiveUsersIsNullThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SessionManagerService(null, Mock.Of<IUtcDateTimeProvider>()));
        }

        [Test]
        public void WhenUtcDateTimeProviderIsNullThenThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SessionManagerService(Mock.Of<IActiveUsers>(), null));
        }

        [Test]
        public void WhenUserDoesntExistInActiveUserDictionaryThenReturnFalse()
        {
            var sessionService = new SessionManagerService(Mock.Of<IActiveUsers>(u => u.Users == new Dictionary<string, DateTime>()), Mock.Of<IUtcDateTimeProvider>());

            sessionService.IsUserActive("userTest").Should().BeFalse();
        }

        [Test]
        public void WhenUserExistInActiveUsersAndLastActivityIsInLast10MinsThenReturnTrue()
        {
            var lastActivity = new DateTime(2016, 9, 10, 10, 0, 0);
            var sessionService = new SessionManagerService(Mock.Of<IActiveUsers>(u =>
                u.Users == new Dictionary<string, DateTime>()
                {
                    {"userTest", lastActivity}
                } && u["userTest"] == lastActivity
            ), Mock.Of<IUtcDateTimeProvider>(d => d.UtcDateTimeNow == new DateTime(2016, 9, 10, 10, 9, 59)));

            sessionService.IsUserActive("userTest").Should().BeTrue();
        }

        [Test]
        public void WhenUserExistInACtiveUsersAndLastActivityIsExact10MinsThenReturnTrue()
        {
            var lastActivityDate = new DateTime(2016, 9, 10, 10, 0, 0);
            var sessionService = new SessionManagerService(Mock.Of<IActiveUsers>(u => u.Users == new Dictionary<string, DateTime>
            {
                {"userTest",  lastActivityDate}
            } && u["userTest"] == lastActivityDate), Mock.Of<IUtcDateTimeProvider>(d => d.UtcDateTimeNow == new DateTime(2016, 9, 10, 10, 10, 00)));

            sessionService.IsUserActive("userTest").Should().BeTrue();
        }

        [Test]
        public void WhenUserExistsInActiveUsersAndLastActivityIsAfter10MinsThenReturnFalse()
        {
            var lastActivityDate = new DateTime(2016, 9, 10, 10, 0, 0);
            var sessionService = new SessionManagerService(Mock.Of<IActiveUsers>(u => u.Users == new Dictionary<string, DateTime>
            {
                {"userTest", lastActivityDate }
            } && u["userTest"] == lastActivityDate), Mock.Of<IUtcDateTimeProvider>(d => d.UtcDateTimeNow == new DateTime(2016, 9, 10, 10, 10, 1)));

            sessionService.IsUserActive("userTest").Should().BeFalse();
        }
    }
}
