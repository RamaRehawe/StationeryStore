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

        public bool Exist(string value, int attributeId)
        {
            return _context.ProductAttributes.Any(a => a.Value == value && a.AttributeId == attributeId);
        }

        public ICollection<ProductAttribute> GetProductAttributes(int productId)
        {
            return _context.ProductAttributes.Where(
                pa => pa.ProductAttributeQuantity.ProductId == productId).ToList();
        }
    }
}
