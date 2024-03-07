namespace StationeryStore.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<ImageAttribute> ImageAttributes { get; set; }
    }
}
