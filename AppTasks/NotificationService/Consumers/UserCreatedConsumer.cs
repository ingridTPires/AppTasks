using MassTransit;
using NotificationService.Service;
using Shared.Events;

namespace NotificationService.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly IMailService _mailService;
        private const string SUBJECT = "Novo Usuário";
        private const string MESSAGE = "Olá {0}, <br />Seu cadastro foi realizado com sucesso!";
        public UserCreatedConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }
        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var message = string.Format(MESSAGE, context.Message.Name);
            await _mailService.Send(context.Message.Email, message, SUBJECT);
        }
    }
}
