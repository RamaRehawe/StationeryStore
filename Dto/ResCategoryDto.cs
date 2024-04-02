
namespace StationeryStore.Dto
{
    public class ResCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ResSubCategoryDto> SubCategories { get; set; }
    }
}
