using Backend1.Data;
using Backend1.IService;
using Backend1.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Backend1.Repositories
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(MongoDBContext context)
        {
            _users = context.Users;
        }

        public List<User> GetAllUsers()
        {
            return _users.Find(user => true).ToList();
        }

        public void AddUser(User user)
        {
            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
            _users.InsertOne(user);
        }

        public async Task<User?> Authenticate(string email, string password)
        {
            var user = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.password))
                return null;

            return user;
        }



        public async Task<User?> UpdateUser(string id, User updatedUser)
        {
            var filter = Builders<User>.Filter.Eq(u => u.id, id);
            var update = Builders<User>.Update
                .Set(u => u.name, updatedUser.name)
                .Set(u => u.password, updatedUser.password)
                .Set(u => u.Email, updatedUser.Email);

            var result = await _users.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0 ? updatedUser : null;
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.id, id);
            var result = await _users.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                return new NotFoundObjectResult($"User with ID {id} not found.");
            }

            return new OkObjectResult($"User with ID {id} deleted successfully.");
        }

       

        public async Task<User?> GetByUserName(string username)
        {
            return await _users.Find(u => u.name == username).FirstOrDefaultAsync();
        }
    }
}
