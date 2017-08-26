using System.ServiceModel;
using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Services.UserService
{
    [ServiceContract]
    public interface IUserRepositoryService
    {
        [OperationContract]
        UserDto[] GetAllUsers(params UserRankDto[] acceptableUserRanks);
    }
}
