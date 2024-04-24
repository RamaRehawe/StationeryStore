using Microsoft.EntityFrameworkCore;
using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class ProductAttributeQuantityRepository : BaseRepository, IProductAttributeQuantityRepository
    {
        public ProductAttributeQuantityRepository(DataContext context) : base(context)
        {
        }

        public bool Create(ProductAttributeQuantity productAttributeQuantity)
        {
           _context.ProductAttributesQuantities.Add(productAttributeQuantity);
            return Save();
        }

        public ProductAttributeQuantity GetById(int id)
        {
            return _context.ProductAttributesQuantities.Where(pa => pa.Id == id).FirstOrDefault();
        }


        public bool Update(ProductAttributeQuantity productAttributeQuantity)
        {
            _context.ProductAttributesQuantities.Update(productAttributeQuantity);
            return Save();
        }
        public ICollection<ProductAttributeQuantity> GetAllProductsWithQuantity()
        {
            return _context.ProductAttributesQuantities
                .Include(pa => pa.Product) // Include the related Product
                .ToList();
        }
    }
}
