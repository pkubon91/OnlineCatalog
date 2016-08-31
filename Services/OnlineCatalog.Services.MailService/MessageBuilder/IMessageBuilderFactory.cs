using OnlineCatalog.Common.DataContracts.General;

namespace OnlineCatalog.Services.MailService.MessageBuilder
{
    public interface IMessageBuilderFactory
    {
        IMessageBuilder CreateMessageBuilder(MailMessageType messageType);
    }
}
