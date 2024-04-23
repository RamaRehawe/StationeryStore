﻿using Microsoft.EntityFrameworkCore;
using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) { }

        /*public ICollection<Product> GetProducts()
        {
            return _context.Products.ToList();
        }*/

        public bool AddProduct(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public ICollection<Product> GetAllProducts()
        {
            return _context.Products.OrderBy(p => p.Id)
                .Include(p => p.ProductAttributeQuantities).ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(p => p.Id == id)
                .Include(p => p.SubCategory)
                .Include(p => p.Reviews).Include(p => p.Rates)
                .Include(p => p.ProductAttributeQuantities)
                .FirstOrDefault()!;
        }

        public ICollection<Product> GetProducts(int subId)
        {
            return _context.Products.Where(p => p.SubCategoryId == subId).ToList();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
