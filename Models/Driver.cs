namespace StationeryStore.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public bool DriverStatus { get; set; } // 1->Avilable, 0->Busy
        public string DrivreLicense { get; set; }
        public User UserId { get; set; }
    }
}
