using StationeryStore.Models;

namespace StationeryStore.Dto
{
    public class ResProductAttributeQuantityDto
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public ICollection<ResProductAttributeDto> ProductAttributes { get; set; }
        public ICollection<ResImageAttributeDto> ImageAttributes { get; set; }
    }
}
