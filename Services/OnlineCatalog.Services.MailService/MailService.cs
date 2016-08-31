using System;
using System.Net.Mail;
using OnlineCatalog.Common.DataContracts.General;
using OnlineCatalog.Services.MailService.MailClient;
using OnlineCatalog.Services.MailService.MessageBuilder;

namespace OnlineCatalog.Services.MailService
{
    public class MailService : IMailService
    {
        private readonly IClient _client;
        private readonly IMessageBuilderFactory _messageBuilderFactory;

        public MailService(IClient client, IMessageBuilderFactory messageBuilderFactory)
        {
            if(client == null) throw new ArgumentNullException(nameof(client));
            if (messageBuilderFactory == null) throw new ArgumentNullException(nameof(messageBuilderFactory));

            _client = client;
            _messageBuilderFactory = messageBuilderFactory;
        }

        public void SendMail(MailContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (context.EmailAddress == null) throw new ArgumentException("Email address has to be provided", nameof(context.EmailAddress));
            if (context.SenderAddress == null) throw new ArgumentException("Sender address has to be provided", nameof(context.SenderAddress));

            MailMessage mail = new MailMessage(context.SenderAddress, context.EmailAddress)
            {
                Subject = context.EmailTitle,
                Body = context.EmailContent.MessageBody
            };

            _client.CreateClient().Send(mail);
        }

        public MailContext BuildMessage(MailContext context)
        {
            if(context == null) return new MailContext() {EmailContent = new MailContent(MailMessageType.Empty)};
            if (context.EmailContent == null)
            {
                context.EmailContent = new MailContent(MailMessageType.Empty);
                return context;
                
            }

            context.EmailContent.MessageBody = _messageBuilderFactory.CreateMessageBuilder(context.EmailContent.MessageType).BuildMessageContent(context);
            return context;
        }
    }
}