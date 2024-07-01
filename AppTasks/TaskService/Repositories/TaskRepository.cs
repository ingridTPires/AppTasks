using MassTransit.Initializers;
using MongoDB.Driver;
using Shared.Data;
using Shared.Models;

namespace TaskService.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DbContext _context;
        public TaskRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<UserTask>> Get() => await _context.Tasks.Find(_ => true).ToListAsync();

        public async Task Create(UserTask task) => await _context.Tasks.InsertOneAsync(task);

        public async Task<string> GetUserMail(string id) => await _context.Users.Find(x => x.Id == id).FirstOrDefaultAsync().Select(x => x.Email);
    }
    public interface ITaskRepository
    {
        Task<List<UserTask>> Get();
        Task Create(UserTask task);
        Task<string> GetUserMail(string id);
    }
}
