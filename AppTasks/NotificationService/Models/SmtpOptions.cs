namespace NotificationService.Models
{
    public class SmtpOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string From { get; set; } = "localhost@localhost.com";
    }
}
