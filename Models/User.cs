namespace StationeryStore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; } // Admin, ItemManeger, Customer, Driver
        public string? JwtToken { get; set; }
        public Cart Cart { get; set; }
        public Driver Driver { get; set; }
        public Admin Admin { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Rate> Rates { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<CustomerService> CustomerServices { get; set; }
    }
}
