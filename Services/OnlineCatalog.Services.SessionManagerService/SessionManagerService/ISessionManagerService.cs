using System.ServiceModel;
using System.ServiceModel.Web;

namespace OnlineCatalog.Services.SessionManagerService
{
    [ServiceContract(Namespace = "https://online.catalog.com/", Name = "SessionManagerService")]
    public interface ISessionManagerService
    {
        [OperationContract]
        [WebGet(UriTemplate = Routing.ClientRoute, BodyStyle = WebMessageBodyStyle.Bare)]
        bool IsUserActive(string userName);

//        [OperationContract]
//
//        void ActivateUser(string userName);
    }
}
