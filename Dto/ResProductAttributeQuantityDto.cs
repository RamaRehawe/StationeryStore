using StationeryStore.Models;

namespace StationeryStore.Dto
{
    public class ResProductAttributeQuantityDto
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Name1 { get; set; }
        public string Value1 { get; set; }
        public string? Name2 { get; set; }
        public string? Value2 { get; set; }
        public ICollection<ResImageAttributeDto> ImageAttributes { get; set; }
    }
}
