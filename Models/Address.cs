namespace StationeryStore.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Title { get; set; } // nullable
        public string City { get; set; }
        public string Street { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public User UserID { get; set; }
    }
}
