using Microsoft.EntityFrameworkCore;
using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class OrderRepository : BaseRepository, IOrderRepository

    {
        public OrderRepository(DataContext context) : base(context)
        {
        }

        public void AddItemToOrder(OrderItem item)
        {
            _context.OrderItems.Add(item);
            _context.SaveChanges();
        }

        public int CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.Id;
        }

        public ICollection<Order> GetDeliverdOrdersByUserId(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId && o.OrderStatus == "Deliverd").ToList();
        }

        public ICollection<Order> GetNotDeliverdOrdersByUserId(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId && o.OrderStatus != "Deliverd").ToList();
        }

        public Order GetOrderByOrderId(int userId, int orderId)
        {
            return _context.Orders.Where(o => o.UserId == userId && o.Id == orderId)
                .Include(o => o.OrderItems).FirstOrDefault();
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.OrderBy(o => o.OrderStatus).ToList();
        }

        public ICollection<Order> GetOrdersByUserId(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }
    }
}
