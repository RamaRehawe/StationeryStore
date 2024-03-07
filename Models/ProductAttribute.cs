namespace StationeryStore.Models
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int QuantityId { get; set; }
        public int AttributeId { get; set; }
        public ProductAttributeQuantity Quantity { get; set; }
        public Atribute Attribute { get; set; }
    }
}
