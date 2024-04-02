using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IAddressRepository : IBaseRepository
    {
        ICollection<Address> GetAddresses();
        Address GetAddress(int id);
        ICollection<Address> GetAddressByUser(int userId);
        bool AddressExists(int id);
        bool AddAddress(Address address);
    }
}
