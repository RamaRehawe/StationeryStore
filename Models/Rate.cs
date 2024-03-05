namespace StationeryStore.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public double Raring { get; set; }
        public DateTime Date { get; set; }
        public User UserId { get; set; }
        public Product ProductId { get; set; }
    }
}
