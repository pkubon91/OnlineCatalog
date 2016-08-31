using System.Net.Mail;

namespace OnlineCatalog.Services.MailService.MailClient
{
    public interface IClient
    {
        SmtpClient CreateClient();
    }
}
