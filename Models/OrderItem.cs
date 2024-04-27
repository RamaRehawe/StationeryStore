namespace StationeryStore.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool OrderedByUser { get; set; } // Track if the user has ordered this product
        public int ProductAttributeQuantityId { get; set; }
        public int OrderId { get; set; }
        public ProductAttributeQuantity ProductAttributeQuantity { get; set; }
        public Order Order { get; set; }
    }
}
