using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Web;
using OnlineCatalog.Common.DataContracts.General;

namespace OnlineCatalog.Services.MailService
{
    [ServiceContract]
    public interface IMailService
    {
        [OperationContract(Action = "SendMail")]
        void SendMail(MailContext context);

        [OperationContract(Action = "BuildMessage")]
        MailContext BuildMessage(MailContext messageType);
    }
}
