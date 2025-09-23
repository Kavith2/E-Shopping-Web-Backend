using Backend1.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend1.IService
{
    public interface IUserService
    {

        List<User> GetAllUsers();
        void AddUser (User user);

        Task<User> UpdateUser(string id, User updateUser);

        Task<IActionResult> DeleteUser (string id);

        Task<User?> GetByUserName (string username);

        Task<User?> Authenticate(string email, string password);


    }
}
