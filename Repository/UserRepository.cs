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

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            Save();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Users
                .Include(u => u.Cart)
                .Include(u => u.Driver)
                .Include(u => u.Admin)
                .FirstOrDefaultAsync(u => u.Email == username);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task UpdateTokenByUsernameAsync(string username, string token)
        {
            var user = await _context.Users.Where(u => u.Email == username).FirstAsync();
            user.JwtToken = token;
            _context.Users.Update(user);
        }
    }
}
