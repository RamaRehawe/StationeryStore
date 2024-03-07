namespace StationeryStore.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public double Raring { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
