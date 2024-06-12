using MassTransit;
using NotificationService.Service;
using Shared.Events;

namespace NotificationService.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly IMailService _mailService;
        public UserCreatedConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }
        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            await _mailService.Send();
        }
    }
}
