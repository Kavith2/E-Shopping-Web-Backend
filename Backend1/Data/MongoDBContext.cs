using Backend1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Backend1.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDB");
            var databaseName = configuration.GetConnectionString("DatabaseName");

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Cart> Cart => _database.GetCollection<Cart>("Cart");

    }


}
