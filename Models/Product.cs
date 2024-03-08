namespace StationeryStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Rate> Rates { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<ProductAttributeQuantity> ProductAttributeQuantities { get; set; }
    }
}
