namespace MTCBackend.Services
{
    using Microsoft.EntityFrameworkCore;
    using MTCBackend.Data;
    using MTCBackend.Interfaces;
    using MTCBackend.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly MTCContext _context;

        public UserService(MTCContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetUsersByType(UserType userType)
        {
            return await _context.Users
                                 .Where(u => u.Role == userType)
                                 .ToListAsync();
        }
    }

}
