using StationeryStore.Models;

namespace StationeryStore.Dto
{
    public class ResSubCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ResProductDto> Products { get; set; }

    }
}
