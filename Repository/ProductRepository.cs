using Microsoft.EntityFrameworkCore;
using StationeryStore.Data;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Models;
using System.Text.RegularExpressions;

namespace StationeryStore.Repository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) { }

        /*public ICollection<Product> GetProducts()
        {
            return _context.Products.ToList();
        }*/

        public bool AddProduct(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public ICollection<Product> GetAllProducts(string search)
        {
            if (string.IsNullOrEmpty(search))
                return _context.Products.OrderBy(p => p.Id)
                    .Include(p => p.ProductAttributeQuantities).ToList();
            var regex = new Regex(search, RegexOptions.IgnoreCase);
            var products = _context.Products.Include(p => p.ProductAttributeQuantities).AsEnumerable()
                                     .Where(p => regex.IsMatch(p.Description) || regex.IsMatch(p.Name))
                                     .ToList();
            return products;
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(p => p.Id == id)
                .Include(p => p.SubCategory)
                .Include(p => p.Reviews).Include(p => p.Rates)
                .Include(p => p.ProductAttributeQuantities)
                .ThenInclude(p => p.ProductAttributes)
                .ThenInclude(p => p.Attribute)
                .FirstOrDefault()!;
        }

        public ICollection<Product> GetProducts(int subId)
        {
            return _context.Products.Where(p => p.SubCategoryId == subId).ToList();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
