using MassTransit;
using NotificationService.Service;
using Shared.Events;

namespace NotificationService.Consumers
{
    public class UserTaskCreatedConsumer : IConsumer<UserTaskCreated>
    {
        private readonly IMailService _mailService;
        private const string SUBJECT = "Nova Tarefa";
        private const string MESSAGE = "Você possui uma nova tarefa!<br />Título: {0}<br />Descrição:{1}";
        public UserTaskCreatedConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }
        public async Task Consume(ConsumeContext<UserTaskCreated> context)
        {
            var message = string.Format(MESSAGE, context.Message.Title, context.Message.Description);
            await _mailService.Send(context.Message.UserMail, message, SUBJECT);
        }
    }
}
