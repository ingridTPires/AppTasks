namespace NotificationService.Service
{
    public class MailService : IMailService
    {
        public Task Send()
        {
            Console.WriteLine($"Enviando e-mail.");
            return Task.CompletedTask;
        }
    }

    public interface IMailService
    {
        Task Send();
    }
}
