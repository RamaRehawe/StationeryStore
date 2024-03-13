using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IAddressRepository
    {
        ICollection<Address> GetAddresses();
        Address GetAddress(int id);
        bool AddressExists(int id);
        bool AddAddress(Address address);
        bool Save();
    }
}
