using OnlineCatalog.Common.DataContracts.General;

namespace OnlineCatalog.Services.MailService.MessageBuilder
{
    public interface IMessageBuilder
    {
        string BuildMessageContent(MailContext context);
    }
}
