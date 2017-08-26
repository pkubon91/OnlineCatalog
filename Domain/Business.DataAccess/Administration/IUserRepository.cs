using System;
using System.Collections.Generic;
using Business.Administration;

namespace Business.DataAccess.Administration
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByLogin(string login);

        IEnumerable<User> GetUsersAssignedToRank(params UserRank[] userRanks);

        User GetUser(Guid userGuid);
    }
}
