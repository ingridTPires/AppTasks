using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;
using Shared.Models;
using TaskService.Repositories;

namespace TaskService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public TaskController(ITaskRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<List<UserTask>> Get() => await _repository.Get();

        [HttpPost]
        public async Task<IActionResult> Post(UserTask task)
        {
            if (task == null || string.IsNullOrEmpty(task.Title) || string.IsNullOrEmpty(task.UserId))
            {
                return BadRequest();
            }

            await _repository.Create(task);

            var userMail = await _repository.GetUserMail(task.UserId);

            if (string.IsNullOrEmpty(userMail))
            {
                return BadRequest("Usuário não localizado");
            }

            await _publishEndpoint.Publish(new UserTaskCreated()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                UserMail = userMail
            });

            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }
    }
}
