using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IProductAttributeQuantityRepository : IBaseRepository
    {
        int Create(ProductAttributeQuantity productAttributeQuantity);
        ProductAttributeQuantity GetById(int id);
        bool Update(ProductAttributeQuantity productAttributeQuantity);
        
    }
}
