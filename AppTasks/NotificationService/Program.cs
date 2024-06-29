using MassTransit;
using NotificationService.Consumers;
using NotificationService.Models;
using NotificationService.Service;

namespace NotificationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<UserCreatedConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("user-created-queue", e =>
                    {
                        e.ConfigureConsumer<UserCreatedConsumer>(context);
                    });
                });
            });

            builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
            builder.Services.AddSingleton<IMailService, SmtpMailService>();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
