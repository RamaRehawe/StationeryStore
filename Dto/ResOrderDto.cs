using StationeryStore.Models;

namespace StationeryStore.Dto
{
    public class ResOrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; } // Price
        public string OrderStatus { get; set; } // Pending, Shipped, Delivered
        public ICollection<ResOrderItemDto> OrderItems { get; set; }

    }
}
