using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
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
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }
    }
}
