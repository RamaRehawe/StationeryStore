using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
    }
}
