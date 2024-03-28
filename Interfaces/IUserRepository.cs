using StationeryStore.Dto;
using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        ICollection<User> GetUsers();
        User GetUserByUsernameAsync(string username);
        Task UpdateTokenByUsernameAsync(string username, string  token);
        bool AddUser(User user);
    }

}
