using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IProductRepository : IBaseRepository
    {
        ICollection<Product> GetProducts();
    }
}
