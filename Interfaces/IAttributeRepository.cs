using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IAttributeRepository : IBaseRepository
    {
        bool CreateAttribute(Atribute attribute);
        ICollection<Atribute> GetAttributes();
        Atribute GetAttribute(int id);
    }
}
