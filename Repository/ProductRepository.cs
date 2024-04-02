using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) { }

        public bool AddProduct(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
