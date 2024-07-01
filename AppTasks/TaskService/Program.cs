using MassTransit;
using Shared.Data;
using TaskService.Repositories;

namespace TaskService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<DBSettings>(builder.Configuration.GetSection("DBSettings"));
            builder.Services.AddSingleton<DbContext>();
            builder.Services.AddSingleton<ITaskRepository, TaskRepository>();

            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq();
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
