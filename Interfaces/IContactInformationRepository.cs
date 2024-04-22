using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IContactInformationRepository : IBaseRepository
    {
        void UpdateInfos(ContactInformation contactInfo);
        ContactInformation GetInfos();
        ContactInformation GetInfoById(int id);
    }
}
