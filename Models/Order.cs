namespace StationeryStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; } // Price
        public string OrderStatus { get; set; } // Pending, Shipped, Delivered
        public int ShippingCost { get; set; }
        public string FailDeliver { get; set; }
        public int DriverId { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public Driver Driver { get; set; }
        public Address Address { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
