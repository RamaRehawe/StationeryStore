namespace StationeryStore.Models
{
    public class ProductAttributeQuantity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Product ProductId { get; set;}
    }
}
