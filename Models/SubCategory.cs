namespace StationeryStore.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category CategoryId { get; set; }
    }
}
