using MTCBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MTCBackend.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<IEnumerable<User>> GetUsersByType(UserType userType);
    }
}
