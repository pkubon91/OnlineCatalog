using NHibernate;

namespace Business.NHibernate
{
    public class SessionFactory : ISessionProvider
    {
        public ISession CreateSession()
        {
            return SessionProvider.OpenSession();
        }
    }
}
