namespace StationeryStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public int ProductAttributeQuantityId { get; set; }
        public Cart Cart { get; set; }
        public ProductAttributeQuantity ProductAttributeQuantity { get; set; }

    }
}
