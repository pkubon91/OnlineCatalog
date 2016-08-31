using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineCatalog.Common.DataContracts.General;
using OnlineCatalog.Services.MailService.MailClient;
using OnlineCatalog.Services.MailService.MessageBuilder;

namespace OnlineCatalog.Services.MailService.Tests.Unit
{
    [TestFixture]
    public class MailServiceTests
    {
        [Test]
        public void WhenClientIsNullThenThrowArgumentNullException()
        {
            Action a = () => new MailService(null, Mock.Of<IMessageBuilderFactory>());

            a.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenMessageBuilderFactoryIsNullThenThrowArgumentNullException()
        {
            Action a = () => new MailService(Mock.Of<IClient>(), null);

            a.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenMailContextIsNullThenThrowArgumentNullException()
        {
            MailService mailService = new MailService(Mock.Of<IClient>(), Mock.Of<IMessageBuilderFactory>());

            Action a = () => mailService.SendMail(null);

            a.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenEmailAddressIsNullThenThrowArgumentException()
        {
            MailService mailService = new MailService(Mock.Of<IClient>(), Mock.Of<IMessageBuilderFactory>());

            Action a = () => mailService.SendMail(new MailContext());

            a.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void WhenSenderAddressIsNullThenThrowArgumenException()
        {
            MailService mailService = new MailService(Mock.Of<IClient>(), Mock.Of<IMessageBuilderFactory>());

            Action a = () => mailService.SendMail(new MailContext() {EmailAddress = "test"});

            a.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void WhenContextIsNullThenEmptyInstanceOfMailContextIsReturned()
        {
            MailService mailService = new MailService(Mock.Of<IClient>(), Mock.Of<IMessageBuilderFactory>());

            MailContext mailContext = mailService.BuildMessage(null);

            mailContext.Should().NotBeNull();
            mailContext.EmailContent.Should().NotBeNull();
            mailContext.EmailContent.MessageType.Should().Be(MailMessageType.Empty);
        }

        [Test]
        public void WhenEmailContentIsNullThenEmailContentIsFilledWithEmptyMessageType()
        {
            MailService mailService = new MailService(Mock.Of<IClient>(), Mock.Of<IMessageBuilderFactory>());

            MailContext mailContext = mailService.BuildMessage(new MailContext());

            mailContext.EmailContent.Should().NotBeNull();
            mailContext.EmailContent.MessageType.Should().Be(MailMessageType.Empty);
        }

        [Test]
        public void MessageBodyIsBuildByMessageBuilderReturnedFromFactory()
        {
            MailMessageType mailMessageType = It.IsAny<MailMessageType>();
            var mailContext = new MailContext()
            {
                EmailContent = new MailContent(mailMessageType)
            };
            IMessageBuilder messageBuilder = Mock.Of<IMessageBuilder>(builder => builder.BuildMessageContent(mailContext) == "test message");
            MailService mailService = new MailService(Mock.Of<IClient>(),
                Mock.Of<IMessageBuilderFactory>(factory => factory.CreateMessageBuilder(mailMessageType) == messageBuilder));

            MailContext context = mailService.BuildMessage(mailContext);

            Mock.Get(messageBuilder).Verify(x => x.BuildMessageContent(mailContext), Times.Once);
            context.EmailContent.MessageBody.Should().Be("test message");
        }
    }
}
