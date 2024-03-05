namespace StationeryStore.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public User UserId { get; set; }
        public Product ProductId { get; set; }
    }
}
