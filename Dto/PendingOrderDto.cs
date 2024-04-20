namespace StationeryStore.Dto
{
    public class PendingOrderDto
    {
        public string OrderStatus { get; set; } // Pending, Shipped, Delivered
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
    }
}
