using Microsoft.EntityFrameworkCore;
using StationeryStore.Data;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void AddDriver(Driver driver)
        {
            _context.Drivers.Add(driver);
            _context.SaveChanges();
        }

        public bool AddUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public User GetUserByUsernameAsync(string username)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Users.FirstOrDefault(x => x.Email == username);
                //.Include(u => u.Cart)
                //.Include(u => u.Driver)
                //.Include(u => u.Admin)
                //.FirstOrDefaultAsync(u => u.Email == username);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        public async Task UpdateTokenByUsernameAsync(string username, string token)
        {
            var user = await _context.Users.Where(u => u.Email == username).FirstAsync();
            user.JwtToken = token;
            _context.Users.Update(user);

        }
    }
}
