using NHibernate;

namespace Business.NHibernate
{
    public interface ISessionProvider
    {
        ISession CreateSession();
    }
}
