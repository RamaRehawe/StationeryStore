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

        public bool Exist(ProductAttribute productAttribute1, ProductAttribute? productAttribute2, int productId)
        {
            if(productAttribute2 == null)
                return _context.ProductAttributes.Include(i => i.ProductAttributeQuantity)
                    .Any(a => a.Value == productAttribute1.Value && a.AttributeId == productAttribute1.AttributeId &&
                    a.ProductAttributeQuantity.ProductId == productId);
            var list1 = _context.ProductAttributes.Include(p => p.ProductAttributeQuantity)
                .Where(p => p.ProductAttributeQuantity.ProductId == productId)
                .Where(p => p.AttributeId == productAttribute1.AttributeId)
                .Where(p => p.Value == productAttribute1.Value)
                .Select(p => p.ProductAttributeQuantityId)
                .ToList();

            var list2 = _context.ProductAttributes.Include(p => p.ProductAttributeQuantity)
                .Where(p => p.ProductAttributeQuantity.ProductId == productId)
                .Where(p => p.AttributeId == productAttribute2.AttributeId)
                .Where(p => p.Value == productAttribute2.Value)
                .Select(p => p.ProductAttributeQuantityId)
                .ToList();

            foreach(var id1 in  list1)
            {
                if (list2.Contains(id1))
                    return true;
            }
            return false;
        }

        public ICollection<ProductAttribute> GetProductAttributes(int productId)
        {
            return _context.ProductAttributes.Where(
                pa => pa.ProductAttributeQuantity.ProductId == productId).ToList();
        }
    }
}
