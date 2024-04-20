
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

        public bool CreateAttribute(Atribute attribute)
        {
            _context.Add(attribute);
            return Save();
        }

        public Atribute GetAttribute(int id)
        {
            return _context.Atributes.Where(a => a.Id == id).FirstOrDefault();
        }

        public ICollection<Atribute> GetAttributes()
        {
            return _context.Atributes.OrderBy(a => a.Id).ToList();
        }
    }
}
