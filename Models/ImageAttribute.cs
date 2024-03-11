namespace StationeryStore.Models
{
    public class ImageAttribute
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int ProductAttributeQuantityId { get; set; }
        public ProductAttributeQuantity ProductAttributeQuantity { get; set; }
    }
}
