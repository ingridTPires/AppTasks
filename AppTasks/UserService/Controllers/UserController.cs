using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public UserController(IUserRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<List<User>> Get() => await _repository.Get();

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
            {
                return BadRequest();
            }

            await _repository.Create(user);

            await _publishEndpoint.Publish(new UserCreated() { Id = user.Id, Name = user.Name, Email = user.Email });

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }
    }
}
