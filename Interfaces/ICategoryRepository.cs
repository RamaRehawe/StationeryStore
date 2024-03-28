using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface ICategoryRepository : IBaseRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
    }
}
