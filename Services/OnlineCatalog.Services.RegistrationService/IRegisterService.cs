using System.ServiceModel;
using OnlineCatalog.Common.DataContracts;
using OnlineCatalog.Common.DataContracts.Administration;

namespace OnlineCatalog.Services.RegistrationService
{
    [ServiceContract(Namespace = "http://online.catalog.com")]
    public interface IRegisterService
    {
        [OperationContract(Action = "RegisterUser")]
        ServiceActionResult RegisterUser(UserDto userForRegistration);
    }
}
