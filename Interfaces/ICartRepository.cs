﻿using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface ICartRepository : IBaseRepository
    {
        void CreateCart(Cart cart);
        Cart GetCartByUserId(int userId);
        void AddItemToCart(CartItem item);
        void UpdateCartItem(CartItem item);
        CartItem GetCartItemById(int itemId, int cartId);
        ProductAttributeQuantity GetProductAttributeQuantityById(int productAttributeQuantityId);
        CartItem GetCartItemByCartAndProductAttributeQuantity(int cartId, int productAttributeQuantityId);
        IEnumerable<CartItem> GetCartItemsByCartId(int cartId);
        void DeleteCartItem(CartItem item);
        void ClearCart(int userId);
    }
}
