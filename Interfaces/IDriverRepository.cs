using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IDriverRepository : IBaseRepository
    {
        void SetDriverStatus(int driverId, bool status);
        IEnumerable<Order> GetPendingOrders();
        bool SelectOrder(int orderId, int driverId);
        void UpdateOrderStatueToShipped(int orderId);
        void UpdateOrderStatueToDeliverd(int orderId);
        List<Order> GetMyOrders(int driverId);
        ICollection<Driver> GetDrivers();
        void FailDeliver(int orderId, string failDeliver);

    }
}
