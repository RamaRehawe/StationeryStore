using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IProductRepository : IBaseRepository
    {
        //ICollection<Product> GetProducts();
        ICollection<Product> GetProducts(int subId);
        Product GetProduct(int id);
        bool ProductExists(int id);
        bool AddProduct(Product product);
        ICollection<Product> GetAllProducts(); 
    }
}
