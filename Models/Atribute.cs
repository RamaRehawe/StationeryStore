namespace StationeryStore.Models
{
    public class Atribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}
