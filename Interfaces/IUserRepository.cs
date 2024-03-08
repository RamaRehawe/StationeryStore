using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
    }
}
