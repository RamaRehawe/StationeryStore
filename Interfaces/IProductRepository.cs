﻿using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IProductRepository : IBaseRepository
    {
        ICollection<Product> GetProducts();
        Product GetProduct(int id);
        bool ProductExists(int id);
        bool AddProduct(Product product);
    }
}
