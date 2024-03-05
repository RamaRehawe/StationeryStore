namespace StationeryStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Cart CartId { get; set; }
        public Product ProductId { get; set; }
    }
}
