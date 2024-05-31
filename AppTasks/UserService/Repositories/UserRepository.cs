using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;
        public UserRepository(IOptions<DBSettings> dbSetting)
        {
            var client = new MongoClient(dbSetting.Value.ConnectionString);
            var db = client.GetDatabase(dbSetting.Value.DatabaseName);
            _collection = db.GetCollection<User>(dbSetting.Value.CollectionName);
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
