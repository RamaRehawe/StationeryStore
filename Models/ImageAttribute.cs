namespace StationeryStore.Models
{
    public class ImageAttribute
    {
        public string Name { get; set; }
        public ProductAttribute ProductAttributeId { get; set; }
        public Image ImageId { get; set; }
    }
}
