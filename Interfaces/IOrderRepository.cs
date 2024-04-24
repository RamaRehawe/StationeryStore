using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IOrderRepository : IBaseRepository
    {
        int CreateOrder(Order order);
        ICollection<Order> GetOrdersByUserId(int userId);
        ICollection<Order> GetDeliverdOrdersByUserId(int userId);
        ICollection<Order> GetNotDeliverdOrdersByUserId(int userId);
        Order GetOrderByOrderId(int userId, int orderId);
        void AddItemToOrder(OrderItem item);
        ICollection<Order> GetOrders();
    }

}
