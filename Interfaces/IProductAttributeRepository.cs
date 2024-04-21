using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IProductAttributeRepository : IBaseRepository
    {
        ICollection<ProductAttribute> GetProductAttributes(int productId);
        void AddProductAttribute(ProductAttribute attributeProduct);
        bool Exist(string value);
    }
}
