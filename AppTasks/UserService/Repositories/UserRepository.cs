using MongoDB.Driver;
using Shared.Data;
using Shared.Models;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;
        public UserRepository(DbContext context)
        {
            _collection = context.Users;
        }

        public async Task<List<User>> Get() => await _collection.Find(_ => true).ToListAsync();

        public async Task Create(User user) => await _collection.InsertOneAsync(user);
    }
    public interface IUserRepository
    {
        Task<List<User>> Get();
        Task Create(User user);
    }
}
