using System.Net.Mail;

namespace OnlineCatalog.Services.MailService.MailClient
{
    public class Client : IClient
    {
        public SmtpClient CreateClient()
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.google.com";
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            return client;
        }
    }
}