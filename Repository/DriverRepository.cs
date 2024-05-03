using Microsoft.EntityFrameworkCore;
using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Repository
{
    public class DriverRepository : BaseRepository, IDriverRepository
    {
        public DriverRepository(DataContext context) : base(context)
        {
        }

        public void FailDeliver(int orderId, string failDeliver)
        {
            var order = _context.Orders.Where(o => o.Id == orderId).FirstOrDefault();
            if (order != null)
            {
                order.FailDeliver = failDeliver;
                order.OrderStatus = "Pending";
                _context.SaveChanges();
            }
        }

        public ICollection<Driver> GetDrivers()
        {
            return _context.Drivers.OrderBy(d => d.Id).ToList();
        }

        public List<Order> GetMyOrders(int driverId)
        {
            var orders =  _context.Orders.Where(o => o.DriverId == driverId)
                .Include(o => o.Address)
                .Include(o => o.OrderItems)
                    .ThenInclude(o => o.ProductAttributeQuantity)
                        .ThenInclude(o => o.ProductAttributes)
                            .ThenInclude(o => o.Attribute)
                .Include(o => o.OrderItems)
                    .ThenInclude(o => o.ProductAttributeQuantity)
                        .ThenInclude(o => o.Product)
                .ToList();
            //foreach(var order in orders)
            //{
            //    var list = order.OrderItems.ToList();
            //    for (int i = 0; i < order.OrderItems.Count; i++)
            //    {
                    
            //    }
            //    order.OrderItems = list;
            //}

            return orders;

        }

        public IEnumerable<Order> GetPendingOrders()
        {
            return _context.Orders
                .Where(o => o.OrderStatus == "loading")
                .Where(o => o.DriverId == null).Include(o => o.User)
                .Include(o => o.Address).ToList();
        }

        public bool SelectOrder(int orderId, int driverId)
        {
            var order = _context.Orders.Where(o => o.Id ==  orderId).FirstOrDefault();

            if (order != null) 
            {
                order.DriverId = driverId;
                order.OrderStatus = "Pending";
                var driver = _context.Drivers.Where(d => d.Id == driverId).FirstOrDefault()!;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void SetDriverStatus(int driverId, bool status)
        {
            var driver = _context.Drivers.Where(d => d.Id == driverId).FirstOrDefault();
            if (driver != null)
            {
                driver.DriverStatus = status;
                _context.SaveChanges();
            }
        }

        public void UpdateOrderStatueDeliverd(int orderId)
        {
            var order = _context.Orders.Where(o => o.Id == orderId).FirstOrDefault();
            if (order != null)
            {
                order.OrderStatus = "Deliverd";
                _context.SaveChanges();
            }
        }

        public void UpdateOrderStatueToDeliverd(int orderId)
        {
            var order = _context.Orders.Where(o => o.Id == orderId).FirstOrDefault();
            if (order != null)
            {
                order.OrderStatus = "Deliverd";
                var orderItems = _context.OrderItems.Where(oi => oi.OrderId == orderId).ToList();
                foreach (var orderItem in orderItems)
                {
                    orderItem.OrderedByUser = true;
                }
                order.FailDeliver = "";
                _context.SaveChanges();

            }
        }

        public void UpdateOrderStatueToShipped(int orderId)
        {
            var order = _context.Orders.Where(o => o.Id == orderId).FirstOrDefault();
            if (order != null)
            {
                order.OrderStatus = "Shipped";
                _context.SaveChanges();
            }
        }
    }
}
