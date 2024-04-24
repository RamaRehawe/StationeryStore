﻿using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IProductAttributeQuantityRepository : IBaseRepository
    {
        bool Create(ProductAttributeQuantity productAttributeQuantity);
        ProductAttributeQuantity GetById(int id);
        bool Update(ProductAttributeQuantity productAttributeQuantity);
        ICollection<ProductAttributeQuantity> GetAllProductsWithQuantity();

    }
}