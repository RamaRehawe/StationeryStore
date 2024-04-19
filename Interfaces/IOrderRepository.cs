using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IOrderRepository : IBaseRepository
    {
        int CreateOrder(Order order);
        ICollection<Order> GetOrdersByUserId(int userId);
        Order GetOrderByOrderId(int userId, int orderId);
        void AddItemToOrder(OrderItem item);
    }

}
