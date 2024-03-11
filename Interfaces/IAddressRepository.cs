using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IAddressRepository
    {
        ICollection<Address> GetAddresses();
        bool AddressExists(int id);
        bool AddAddress(Address address);
        bool Save();
    }
}
