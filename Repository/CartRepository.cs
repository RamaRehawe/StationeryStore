
using Microsoft.EntityFrameworkCore;
using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class CartRepository : BaseRepository, ICartRepository
    {
        public CartRepository(DataContext context) : base(context)
        {
        }

        public void AddItemToCart(CartItem item)
        {
            
            _context.CartItems.Add(item);
            _context.SaveChanges();
        }

        public void ClearCart(int userId)
        {
            var cartItems = _context.CartItems.Where(ci => ci.Cart.UserId == userId).ToList();
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public void CreateCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void DeleteCartItem(CartItem item)
        {
            _context.CartItems.Remove(item);
            _context.SaveChanges();
        }

        public Cart GetCartByUserId(int userId)
        {
            return _context.Carts.FirstOrDefault(c => c.UserId == userId);
        }

        public CartItem GetCartItemByCartAndProductAttributeQuantity(int cartId, int productAttributeQuantityId)
        {
            return _context.CartItems.FirstOrDefault(ci => ci.CartId == cartId && ci.ProductAttributeQuantityId == productAttributeQuantityId);
        }

        public CartItem GetCartItemById(int itemId, int cartId)
        {
            return _context.CartItems.Where(ci => ci.CartId == cartId && ci.ProductAttributeQuantity.ProductId == itemId).FirstOrDefault();
            //return _context.CartItems.FirstOrDefault(ci => ci.Id == itemId);
        }

        public IEnumerable<CartItem> GetCartItemsByCartId(int cartId)
        {
            return _context.CartItems.Where(item => item.CartId == cartId).ToList();
        }

        public ProductAttributeQuantity GetProductAttributeQuantityById(int productAttributeQuantityId)
        {
            return _context.ProductAttributesQuantities
                .Include(pa => pa.Product)
                .FirstOrDefault(pa => pa.Id == productAttributeQuantityId);
        }

        public void UpdateCartItem(CartItem item)
        {
            _context.CartItems.Update(item);
            _context.SaveChanges();
        }
    }
}
