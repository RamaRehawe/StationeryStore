using Microsoft.EntityFrameworkCore;
using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Users
                .Include(u => u.Cart)
                .Include(u => u.Driver)
                .Include(u => u.Admin)
                .FirstOrDefaultAsync(u => u.Username == username);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }
    }
}
