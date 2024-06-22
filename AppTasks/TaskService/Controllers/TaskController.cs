using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using TaskService.Repositories;

namespace TaskService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskRepository _repository;
        public TaskController(ITaskRepository repository)
        {
            _repository = repository;
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

            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }
    }
}
