namespace StationeryStore.Models
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int ProductAttributeQuantityId { get; set; }
        public int AttributeId { get; set; }
        public ProductAttributeQuantity ProductAttributeQuantity { get; set; }
        public Atribute Attribute { get; set; }
    }
}
