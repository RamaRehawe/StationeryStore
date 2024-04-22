using StationeryStore.Models;

namespace StationeryStore.Interfaces
{
    public interface IDriverRepository : IBaseRepository
    {
        void SetDriverStatus(int driverId, bool status);
        IEnumerable<Order> GetPendingOrders();
        void SelectOrder(int orderId, int driverId);
        void UpdateOrderStatue(int orderId);
        ICollection<Order> GetMyOrders(int driverId);
        ICollection<Driver> GetDrivers();
        void AddDriver(Driver driver); // Add this method for adding a driver

    }
}
