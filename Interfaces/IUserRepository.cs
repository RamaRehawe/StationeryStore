﻿using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        Task<User> GetUserByUsernameAsync(string username);
        Task UpdateTokenByUsernameAsync(string username, string  token);
        void AddUser(User user);
        bool Save();
    }

}
