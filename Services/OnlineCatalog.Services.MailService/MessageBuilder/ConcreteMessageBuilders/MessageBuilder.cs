using System.Text;

namespace OnlineCatalog.Services.MailService.MessageBuilder.ConcreteMessageBuilders
{
    public abstract class MessageBuilder
    {
        protected StringBuilder _stringBuilder = new StringBuilder();

        protected MessageBuilder()
        {
            _stringBuilder.AppendFormat("Hello");
        }

        protected StringBuilder Finalize()
        {
            return _stringBuilder.AppendLine("This is auto generated e-mail, please do not respond");
        }
    }
}