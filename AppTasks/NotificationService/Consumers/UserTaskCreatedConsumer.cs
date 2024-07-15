using MassTransit;
using NotificationService.Service;
using Shared.Events;

namespace NotificationService.Consumers
{
    public class UserTaskCreatedConsumer : IConsumer<UserTaskCreated>
    {
        private readonly IMailService _mailService;
        ILogger<UserTaskCreatedConsumer> _logger;
        private const string SUBJECT = "Nova Tarefa";
        private const string MESSAGE = "Você possui uma nova tarefa!<br />Título: {0}<br />Descrição:{1}";
        public UserTaskCreatedConsumer(IMailService mailService, ILogger<UserTaskCreatedConsumer> logger)
        {
            _mailService = mailService;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<UserTaskCreated> context)
        {
            _logger.LogInformation($"event: tarefa recebida {context.Message.Id} para o usuário {context.Message.UserMail}");

            var message = string.Format(MESSAGE, context.Message.Title, context.Message.Description);
            await _mailService.Send(context.Message.UserMail, message, SUBJECT);
        }
    }
}
