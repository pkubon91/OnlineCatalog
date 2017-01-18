using System;
using System.ServiceModel;
using OnlineCatalog.Common.DataContracts;

namespace OnlineCatalog.Services.UserService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IUserAssignmentService
    {
        [OperationContract]
        ServiceActionResult AssignUserToShop(string login, Guid shopGuid);

        [OperationContract]
        ServiceActionResult UnassignUserFromShop(string login, Guid shopGuid);
    }
}