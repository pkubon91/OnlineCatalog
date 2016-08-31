using Business.Administration;

namespace Business.DataAccess.Administration
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByLogin(string login);
    }
}
