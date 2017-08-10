using Business.DataAccess.Administration;
using Business.NHibernate;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Services.LoginService.Tests.Integration
{
    [TestFixture]
    public class LoginServiceTests
    {
        [Test]
        public void WhenUserDoesNotExists_ThenEmptyUserDtoIsReturned()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            LoginService service = CreateService();

            var user = service.LoginUser("NotExistingUser", "testPassword");

            Assert.AreEqual(UserDto.EmptyUser, user);
            Assert.AreEqual(false, user.IsAuthenticated);
        }

        [Test]
        public void WhenUserExistsAndPasswordMatched_ThenReturnUserIsAuthenticated()
        {
            LoginService service = CreateService();

            var user = service.LoginUser("pkubonAdmin", "pcim846");

            Assert.AreEqual(true, user.IsAuthenticated);
        }

        private LoginService CreateService()
        {
            return new LoginService(new UserRepository(new SessionFactory()));
        }
    }
}
