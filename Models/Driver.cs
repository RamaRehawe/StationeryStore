namespace StationeryStore.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public bool DriverStatus { get; set; } // 1->Avilable, 0->Busy
        public string DrivreLicense { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
