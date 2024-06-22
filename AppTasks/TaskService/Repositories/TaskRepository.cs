using MongoDB.Driver;
using Shared.Data;
using Shared.Models;

namespace TaskService.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<UserTask> _collection;
        public TaskRepository(DbContext context)
        {
            _collection = context.Tasks;
        }

        public async Task<List<UserTask>> Get() => await _collection.Find(_ => true).ToListAsync();

        public async Task Create(UserTask task) => await _collection.InsertOneAsync(task);
    }
    public interface ITaskRepository
    {
        Task<List<UserTask>> Get();
        Task Create(UserTask task);
    }
}
