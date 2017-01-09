using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OnlineCatalog.Common.DataContracts;

namespace OnlineCatalog.Services.UserService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IUserAssignmentService
    {
        [OperationContract]
        ServiceActionResult AssignUserToShop(string login, Guid shopGuid);
    }
}