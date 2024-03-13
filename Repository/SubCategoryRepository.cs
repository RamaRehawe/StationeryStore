using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly DataContext _context;

        public SubCategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateSubCategory(SubCategory subCategory)
        {
            _context.Add(subCategory);
            return Save();
        }

        public SubCategory GetSubCategory(int id)
        {
            return _context.SubCategories.Where(s => s.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<SubCategory> GetSubCategories()
        {
            return _context.SubCategories.OrderBy(s => s.Name).ToList();
        }

        public bool SubCategoryExists(int id)
        {
            return _context.SubCategories.Any(s =>  s.Id == id);
        }
    }
}
