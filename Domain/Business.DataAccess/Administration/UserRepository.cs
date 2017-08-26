using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Business.Administration;
using Business.NHibernate;
using NHibernate;
using NHibernate.Criterion;
using OnlineCatalog.Common.Extensions;

namespace Business.DataAccess.Administration
{
    public class UserRepository : IUserRepository
    {
        private readonly ISessionProvider _sessionProvider;

        public UserRepository(ISessionProvider sessionProvider)
        {
            if (sessionProvider == null) throw new ArgumentNullException(nameof(sessionProvider));
            _sessionProvider = sessionProvider;
        }

        public void AddToDatabase(User entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using (var session = _sessionProvider.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        public User GetUserByLogin(string login)
        {
            if(StringExtensions.IsNullOrEmpty(login)) throw new ArgumentNullException(nameof(login));

            User user;
            using (var session = _sessionProvider.CreateSession())
            {
                var usersQuery = session.CreateQuery(@"select u from User u  
                                                        where u.Login = :Login");
                usersQuery.SetString("Login", login);
                var users = usersQuery.List<User>();
                if(users.Count > 1) throw new NonUniqueResultException(users.Count);
                user = users.SingleOrDefault();
            }
            return user;
        }

        public IEnumerable<User> GetUsersAssignedToRank(params UserRank[] userRanks)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return session.QueryOver<User>().Where(u => RestrictionExtensions.IsIn((object) u.UserRank, (ICollection) userRanks)).List<User>();
            }
        }

        public User GetUser(Guid userGuid)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return session.Get<User>(userGuid);
            }
        }
    }
}
