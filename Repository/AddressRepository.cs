using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DataContext _context;
        public AddressRepository(DataContext context)
        {
            _context = context;
        }
        public bool AddAddress(Address address)
        {
            _context.Add(address);
            return Save();
        }

        public bool AddressExists(int id)
        {
            return _context.Addresses.Any(a => a.Id == id);
        }

        public Address GetAddress(int id)
        {
            return _context.Addresses.Where(a => a.Id == id).FirstOrDefault();
        }

        public ICollection<Address> GetAddresses()
        {
            return _context.Addresses.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        
    }
}
