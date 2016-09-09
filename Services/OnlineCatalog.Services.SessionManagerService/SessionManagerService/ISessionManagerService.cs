using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace OnlineCatalog.Services.SessionManagerService
{
    [ServiceContract(Namespace = "https://online.catalog.com/", Name = "SessionManagerService")]
    public interface ISessionManagerService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "activeUsers/{userName}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        bool IsUserActive(string userName);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "activateUser/{userName}", BodyStyle = WebMessageBodyStyle.Bare)]
        void ActivateUser(string userName);
    }
}
