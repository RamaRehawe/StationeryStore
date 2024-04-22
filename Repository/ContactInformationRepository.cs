using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class ContactInformationRepository : BaseRepository, IContactInformationRepository
    {
        public ContactInformationRepository(DataContext context) : base(context)
        {
        }

        public ContactInformation GetInfoById(int id)
        {
            return _context.ContactInformation.Where(ci => ci.Id == id).FirstOrDefault()!;
        }

        public ContactInformation GetInfos()
        {
            return _context.ContactInformation.OrderBy(ci => ci.Id).FirstOrDefault()!;
        }

        public void UpdateInfos(ContactInformation contactInfo)
        {
            _context.ContactInformation.Update(contactInfo);
            _context.SaveChanges();
        }
    }
}
