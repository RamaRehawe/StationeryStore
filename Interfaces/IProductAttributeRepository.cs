using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IProductAttributeRepository : IBaseRepository
    {
        ICollection<ProductAttribute> GetProductAttributes(int productId);
        void AddProductAttribute(ProductAttribute attributeProduct);
        bool Exist(ProductAttribute productAttribute1, ProductAttribute? productAttribute2, int productId);
    }
}
