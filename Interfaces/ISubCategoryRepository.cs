using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface ISubCategoryRepository : IBaseRepository
    {
        ICollection<SubCategory> GetSubCategories();
        SubCategory GetSubCategory(int id);
        bool SubCategoryExists(int id);
        bool CreateSubCategory(SubCategory subCategory);
    }
}
