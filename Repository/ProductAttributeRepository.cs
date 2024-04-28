using Microsoft.EntityFrameworkCore;
using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class ProductAttributeRepository : BaseRepository, IProductAttributeRepository
    {
        public ProductAttributeRepository(DataContext context) : base(context)
        {
        }

        public void AddProductAttribute(ProductAttribute attributeProduct)
        {
            _context.ProductAttributes.Add(attributeProduct);
            _context.SaveChanges();
        }

        public bool Exist(string value, int attributeId, int productId)
        {

            return _context.ProductAttributes.Include(i => i.ProductAttributeQuantity)
                .Any(a => a.Value == value && a.AttributeId == attributeId && 
                a.ProductAttributeQuantity.ProductId == productId);
        }

        public ICollection<ProductAttribute> GetProductAttributes(int productId)
        {
            return _context.ProductAttributes.Where(
                pa => pa.ProductAttributeQuantity.ProductId == productId).ToList();
        }
    }
}
