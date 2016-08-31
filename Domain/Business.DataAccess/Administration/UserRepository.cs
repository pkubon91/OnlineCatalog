using System;
using Business.Administration;
using Business.NHibernate;
using NHibernate;
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
            if(login.IsNullOrEmpty()) throw new ArgumentNullException(nameof(login));

            User user;
            using (var session = _sessionProvider.CreateSession())
            {
                var users = session.QueryOver<User>().Where(u => u.Login == login);
                if(users.RowCount() > 1) throw new NonUniqueResultException(users.RowCount());
                user = users.SingleOrDefault();
            }
            return user;
        }
    }
}
