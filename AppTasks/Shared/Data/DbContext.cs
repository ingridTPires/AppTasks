using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared.Models;

namespace Shared.Data
{
    public class DbContext
    {
        private readonly IMongoDatabase _database;

        public DbContext(IOptions<DBSettings> dbSettings)
        {
            var client = new MongoClient(dbSettings.Value.ConnectionString);
            _database = client.GetDatabase(dbSettings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<UserTask> Tasks => _database.GetCollection<UserTask>("Tasks");
    }
}
