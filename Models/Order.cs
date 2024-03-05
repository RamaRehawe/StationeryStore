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
        public Driver DriverId { get; set; }
        public Address AddressId { get; set; }
        public User UserId { get; set; }
    }
}
