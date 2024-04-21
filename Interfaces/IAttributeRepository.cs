using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IAttributeRepository : IBaseRepository
    {
        ICollection<Atribute> GetAttributesForProduct(int productId);
        int AddAttribute(Atribute attribute);
        int GetProductAttributeQuantityByProduct(int productId);
        ICollection<Atribute> GetAttributes();
        Atribute GetAttribute(int id);
        bool Exist(string name);
        int GetAttributeId(string name);
    }
}
