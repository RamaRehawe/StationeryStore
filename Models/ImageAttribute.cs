namespace StationeryStore.Models
{
    public class ImageAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductAttributeQuantityId { get; set; }
        public int ImageId { get; set; }
        public ProductAttributeQuantity ProductAttributeQuantity { get; set; }
        public Image Image { get; set; }
    }
}
