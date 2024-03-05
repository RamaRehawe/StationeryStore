namespace StationeryStore.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public Product ProductId { get; set; }
    }
}
