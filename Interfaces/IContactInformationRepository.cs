using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IContactInformationRepository : IBaseRepository
    {
        void UpdateInfos(ContactInformation contactInfo);
        IEnumerable<ContactInformation> GetInfos(); 
        ContactInformation GetInfoById(int id);
    }
}
