
using Microsoft.EntityFrameworkCore;
using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class AttributeRepository : BaseRepository, IAttributeRepository
    {
        public AttributeRepository(DataContext context) : base(context)
        {
        }

        public int AddAttribute(Atribute attribute)
        {
            _context.Add(attribute);
            _context.SaveChanges();
            return attribute.Id;
        }

        public bool Exist(string name)
        {
            return _context.Atributes.Any(a => a.Name == name);
        }

        public Atribute GetAttribute(int id)
        {
            return _context.Atributes.Where(a => a.Id == id)
               .Include(a => a.ProductAttributes).FirstOrDefault();
        }

        public ICollection<Atribute> GetAttributes()
        {
            return _context.Atributes.OrderBy(a => a.Id).ToList();
        }

        public ICollection<Atribute> GetAttributesForProduct(int productId)
        {
            return _context.Atributes.Where(a => a.ProductAttributes.Any(
                pa => pa.ProductAttributeQuantity.ProductId == productId)).ToList();
        }

        public int GetProductAttributeQuantityByProduct(int productId)
        {
            var res = _context.ProductAttributesQuantities.Where(p => p.ProductId == productId).FirstOrDefault();
            return res.Id;
        }
    }
}
