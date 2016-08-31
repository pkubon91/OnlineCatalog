using System.Net.Mail;
using FluentAssertions;
using NUnit.Framework;
using OnlineCatalog.Services.MailService.MailClient;

namespace OnlineCatalog.Services.MailService.Tests.Unit.MailClient
{
    [TestFixture]
    public class ClientTests
    {
        [Test]
        public void SmtpClientIsSentProperly()
        {
            Client client = new Client();
            SmtpClient smtpClient = client.CreateClient();

            smtpClient.Port.Should().Be(25);
            smtpClient.DeliveryMethod.Should().Be(SmtpDeliveryMethod.Network);
            smtpClient.UseDefaultCredentials.Should().BeFalse();
            smtpClient.Host.Should().Be("smtp.google.com");
        }
    }
}
