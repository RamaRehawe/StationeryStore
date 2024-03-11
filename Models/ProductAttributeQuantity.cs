namespace StationeryStore.Models
{
    public class ProductAttributeQuantity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set;}
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public ICollection<ImageAttribute> ImageAttributes { get; set; }
    }
}
