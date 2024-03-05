namespace StationeryStore.Models
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public ProductAttributeQuantity QuantityId { get; set; }
        public Attribute AttributeId { get; set; }
    }
}
