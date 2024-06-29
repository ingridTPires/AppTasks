using Microsoft.Extensions.Options;
using NotificationService.Models;
using System.Net.Mail;
using System.Text;

namespace NotificationService.Service
{
    public class SmtpMailService : IMailService
    {
        private readonly SmtpOptions _options;
        public SmtpMailService(IOptions<SmtpOptions> options)
        {
            _options = options.Value;
        }

        public async Task Send(string to, string message, string subject)
        {
            using var mail = new MailMessage()
            {
                From = new MailAddress(_options.From),
                Body = message,
                Subject = subject,
                BodyEncoding = Encoding.UTF8,

            };

            mail.To.Add(to);

            using var client = new SmtpClient(_options.Host, _options.Port) { UseDefaultCredentials = true };

            await client.SendMailAsync(mail);
        }
    }

    public interface IMailService
    {
        Task Send(string to, string message, string subject);
    }
}
