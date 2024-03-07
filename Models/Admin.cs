namespace StationeryStore.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public bool Type { get; set; } // 1-> working progress, 0->Done
        public int UserId { get; set; }
        public User User { get; set; }
    }
}