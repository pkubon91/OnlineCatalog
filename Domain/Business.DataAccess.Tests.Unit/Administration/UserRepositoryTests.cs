using System;
using Business.Administration;
using Business.DataAccess.Administration;
using Business.NHibernate;
using FluentAssertions;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace Business.DataAccess.Tests.Unit.Administration
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test]
        public void ctor_WhenSessionProviderIsNullThenArgumentNullExceptionIsThrown()
        { 
            Action a = () => new UserRepository(null);

            a.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void IfStorageContainsMoreRowsThanOneWithTheSameLoginThenNonUniqueResultExceptionIsThrown()
        {
            ISession sessionMock = Mock.Of<ISession>(session => session.QueryOver<User>().Where(user => user.Login == "testLogin").RowCount() == 2);
            ISessionProvider sessionProvider = Mock.Of<ISessionProvider>(provider => provider.CreateSession() == sessionMock);

            var userRepository = new UserRepository(sessionProvider);

            userRepository.Invoking(x => userRepository.GetUserByLogin("testLogin")).ShouldThrow<NonUniqueResultException>();
        }

        [Test]
        public void WhenDatabaseContainsElementWithPassedLoginThenUserWithTheSameLoginIsReturned()
        {
            ISession sessionMock =
                Mock.Of<ISession>(
                    session =>
                        session.QueryOver<User>().Where(user => user.Login == "testLogin").SingleOrDefault() == new User("testLogin", "testPassword", UserRank.Client));
            ISessionProvider sessionProvider = Mock.Of<ISessionProvider>(provider => provider.CreateSession() == sessionMock);

            var userRepository = new UserRepository(sessionProvider);

            userRepository.GetUserByLogin("testLogin").Login.Should().Be("testLogin");
        }

        [TestCase("")]
        [TestCase(null)]
        public void WhenUserLoginIsNullOrEmptyThenArgumentNullExceptionIsThrown(string login)
        {
            UserRepository userRepository = new UserRepository(Mock.Of<ISessionProvider>());

            userRepository.Invoking(repo => repo.GetUserByLogin(login)).ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenUserIsNullThenArgumentNullExceptionIsThrown()
        {
            UserRepository userRepository = new UserRepository(Mock.Of<ISessionProvider>());

            userRepository.Invoking(repo => repo.AddToDatabase(null)).ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SessionOpensTransactionToAddEntityToDb()
        {
            var sessionMock = new Mock<ISession>();
            sessionMock.Setup(session => session.BeginTransaction()).Returns(Mock.Of<ITransaction>());
            UserRepository userRepository = new UserRepository(Mock.Of<ISessionProvider>(provider => provider.CreateSession() == sessionMock.Object));

            userRepository.AddToDatabase(new User(UserRank.SystemAdministrator));

            sessionMock.Verify(session => session.BeginTransaction(), Times.Once);
        }

        [Test]
        public void SessionCommitWholeTransactionAfterOperation()
        {
            var sessionMock = new Mock<ISession>();
            var transactionMock = new Mock<ITransaction>();
            sessionMock.Setup(session => session.BeginTransaction()).Returns(transactionMock.Object);
            UserRepository userRepository = new UserRepository(Mock.Of<ISessionProvider>(provider => provider.CreateSession() == sessionMock.Object));

            userRepository.AddToDatabase(new User(UserRank.Client));

            transactionMock.Verify(transaction => transaction.Commit(), Times.Once);
        }
    }
}
