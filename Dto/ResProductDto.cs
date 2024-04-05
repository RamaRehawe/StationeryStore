using StationeryStore.Models;

namespace StationeryStore.Dto
{
    public class ResProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
        public ResSubCategoryDto SubCategory { get; set; }
        public ICollection<ResRateDto> Rates { get; set; }
        public ICollection<ResReviewDto> Reviews { get; set; }

    }
}
