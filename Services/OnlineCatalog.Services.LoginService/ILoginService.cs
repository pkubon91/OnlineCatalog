using System.ServiceModel;
using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Services.LoginService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface ILoginService
    {
        [OperationContract]
        UserDto LoginUser(string login, string hashPassword);
    }
}
