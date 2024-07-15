using MassTransit;
using NotificationService.Service;
using Shared.Events;

namespace NotificationService.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly IMailService _mailService;
        private readonly ILogger<UserCreatedConsumer> _logger;
        private const string SUBJECT = "Novo Usuário";
        private const string MESSAGE = "Olá {0}, <br />Seu cadastro foi realizado com sucesso!";
        public UserCreatedConsumer(IMailService mailService, ILogger<UserCreatedConsumer> logger)
        {
            _mailService = mailService;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            _logger.LogInformation($"event: usuário recebido {context.Message.Id}");

            var message = string.Format(MESSAGE, context.Message.Name);
            await _mailService.Send(context.Message.Email, message, SUBJECT);
        }
    }
}
