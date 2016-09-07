using System.ServiceModel;
using OnlineCatalog.Common.DataContracts.General;

namespace OnlineCatalog.Services.MailService
{
    [ServiceContract(Namespace = "https://online.catalog.com/")]
    public interface IMailService
    {
        [OperationContract(Action = "SendMail")]
        void SendMail(MailContext context);

        [OperationContract(Action = "BuildMessage")]
        MailContext BuildMessage(MailContext messageType);
    }
}
