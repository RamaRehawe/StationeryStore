﻿using StationeryStore.Models;

namespace StationeryStore.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
